using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIErrorHandling;
using APIErrorHandling.Models;
using APIResponseMessageWrapper;
using Auth.API.Helpers;
using Auth.API.ViewModels;
using AutoMapper;
using CaseSolutionsTokenValidationParameters.Models;
using Database.Service.API.Data.UserData.UserEntities.UserContext;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AccountsController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            UserContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _context = appDbContext;
        }

        // GET api/accounts/ping
        [HttpGet]
        public ActionResult<object> Ping()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.APIMessages.Ping));
        }

        // POST api/accounts/signup
        public async Task<IActionResult> Signup([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = model.Email.Trim();
            if (await UserExists(userEmail))
            {
                return new JsonResult(Errors.SigInErrorResponse(
                    new SigUnAndRoleErrorHandlingResponse()
                    {
                        Error = "User exists",
                        StatusCode = 400,
                        Description = "Please enter a new user email. This user does already exists.",
                        Email = userEmail,
                        Id = "",
                        Code = "user_exists"
                    }));
            }

            var userRole = model.Role.Trim();
            if (!await RoleExists(userRole))
            {
                return new JsonResult(Errors.SigInErrorResponse(
                    new SigUnAndRoleErrorHandlingResponse()
                    {

                        Error = "Role exists",
                        StatusCode = 400,
                        Description = "Please try to add a new role. This role does already exists.",
                        Email = userEmail,
                        Id = "",
                        Code = "role_exists"
                    }));
            }

            var userIdentity = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            }

            var addRoleResult = await _userManager.AddToRoleAsync(userIdentity, userRole);
            if (!addRoleResult.Succeeded)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(addRoleResult, ModelState));
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(Wrappyfier.WrapSigupResponse(userIdentity.Id, userIdentity.Email, 200));
        }

        // POST api/accounts/addrole
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RoleToAddViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = model.RoleToAdd.Trim();
            if (await RoleExists(role))
            {
                return new JsonResult(Errors.AddRoleErrorResponse(
                    new GatewayAddRoleResponse()
                    {
                        Role = role,
                        Code = "role_exits",
                        Description = "Role already exists. Please try to add another role.",
                        Error = "Role exists.",
                        StatusCode = 400
                    }));
            }

            var roleResult = await _roleManager
                .CreateAsync(
                     new IdentityRole
                     {
                         Name = role
                     });

            if (!roleResult.Succeeded)
            {
                return new JsonResult(Errors.AddRoleErrorResponse(
                    new GatewayAddRoleResponse()
                    {
                        Role = role,
                        Code = "faild_to_add_role",
                        Description = "Faild to add role to database.",
                        Error = "Faild to add role.",
                        StatusCode = 424
                    }));
            }

            await _context.SaveChangesAsync();


            return new OkObjectResult(Wrappyfier.WrapAddRoleResponse(role));
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole([FromBody] AddUserToRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            string id = model.Id.Trim();
            string role = model.Role;
            if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(role))
            {
                return new JsonResult(
                    new AddUserToRoleResponseMessage()
                    {
                        Role = role,
                        Email = "no_email",
                        Code = "useremail_or_ role_is_empty",
                        Description = "",
                        Error = "User email or role can not be empty",
                        StatusCode = 400
                    });
            }

            bool roleExists = await RoleExists(role);
            bool userExists = await UserExists(id);

            if (!roleExists && !userExists)
            {
                return new JsonResult(
                      new AddUserToRoleResponseMessage()
                      {
                          Role = role,
                          Email = "no_email",
                          Code = "user_or_role__is_not_found",
                          Description = "The user id or the role name does not match a user or a role.",
                          Error = "User or role is not found",
                          StatusCode = 404
                      });
            }

            var userIdentity = await _userManager.FindByIdAsync(id);
            var userRole = await _roleManager.FindByNameAsync(role);


            if (!await UserHasRole(userIdentity, userRole.Name))
            {
                var addRoleResult = await _userManager.AddToRoleAsync(userIdentity, userRole.Name);

                if (!addRoleResult.Succeeded)
                {
                    return new JsonResult(Errors.AddRoleErrorResponse(
                        new GatewayAddRoleResponse()
                        {
                            Role = role,
                            Code = "faild_to_add_role_to_user",
                            Description = "Faild to add role to user.",
                            Error = "Faild to add role to user.",
                            StatusCode = 424
                        }));
                }
            }
            else
            {
                return new JsonResult(Errors.AddRoleErrorResponse(
                        new GatewayAddRoleResponse()
                        {
                            Role = role,
                            Code = "user_has_already_role_assigned",
                            Description = "User already has the role assigned",
                            Error = "User has already role assigned.",
                            StatusCode = 400
                        }));
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(Wrappyfier.WrapAddRoleToUserResponse(userIdentity.Email, userRole.Name));
        }

        public async Task<bool> UserHasRole(User user, string role)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            bool userHasRole = userRoles.Any(x => x.Equals(role));

            return userHasRole;
        }

        //Helper methods
        public async Task<bool> RoleExists(string role)
        {
            bool roleExist = await _roleManager.RoleExistsAsync(role);

            return roleExist;
        }

        public async Task<bool> UserExists(string userEmailOrId)
        {
            Guid guidId = Guid.Empty;
            User userExist;
            if (Guid.TryParse(userEmailOrId, out guidId))
            {
                userExist = await _userManager.FindByIdAsync(userEmailOrId);
            }
            else
            {
                userExist = await _userManager.FindByEmailAsync(userEmailOrId);

            }

            return userExist == null ? false : true;
        }
        //Helper methods

        //https://aryalnishan.com.np/asp-net-mvc/delete-user-related-data-in-asp-net-mvc-identity/
        //[Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpDelete]
        //Delete  /api/auth/deleteuser
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return NotFound(Wrappyfier
                    .WrapResponse(404, String
                    .Format(Constants.APIMessages.
                    NotFoundMessage, model.Id)));
            }

            //var rolesForUser = await _userManager.GetRolesAsync(user);

            //var removeLoginsResult = await _userManager.RemoveLoginAsync(user, );
            //if (!removeLoginsResult.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(removeLoginsResult, ModelState));

            //var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, rolesForUser);
            //if (!removeLoginsResult.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(removeRolesResult, ModelState));

            //var removeUserResult = await _userManager.DeleteAsync(user);
            //if (!removeLoginsResult.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(removeUserResult, ModelState));

            //await _context.SaveChangesAsync();

            return new OkObjectResult(new { });
        }

    }
}
