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
                        Description = "Please enter a new user email. This user does already exists."
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
                        Description = "Please try to add a new role. This role does already exists."
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

            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.APIMessages.AccountCreatedSuccessMessage));
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
                return new JsonResult(Errors.SigInErrorResponse(
                    new SigUnAndRoleErrorHandlingResponse()
                    {
                        Error = "Role exists",
                        StatusCode = 400,
                        Description = "Please try to add a new role. This role does already exists."
                    }));
            }
              
            IdentityRole newRole = new IdentityRole
            {
                Name = role
            };
            var roleResult = await _roleManager.CreateAsync(newRole);

            if (!roleResult.Succeeded)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(roleResult, ModelState));
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.APIMessages.RoleAdded));
        }

        //Helper methods
        public async Task<bool> RoleExists(string role)
        {
            bool roleExist = await _roleManager.RoleExistsAsync(role);

            return roleExist;
        }

        public async Task<bool> UserExists(string userEmail)
        {
            User userExist = await _userManager.FindByEmailAsync(userEmail);

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
