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
using ResponseModels.Models;
using ResponseModels.ViewModels;

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
                    new SignUpAndRoleErrorHandlingResponse()
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
                    new SignUpAndRoleErrorHandlingResponse()
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
                return new JsonResult(
                    new SignUpAndRoleErrorHandlingResponse()
                    {
                        Error = "Unable to create user",
                        StatusCode = 422,
                        Description = "User could not be created at this time",
                        Email = userEmail,
                        Id = userIdentity.Id,
                        Code = "unable_to_create_user"
                    });
            }

            var addRoleResult = await _userManager.AddToRoleAsync(userIdentity, userRole);
            if (!addRoleResult.Succeeded)
            {
                return new JsonResult(
                    new SignUpAndRoleErrorHandlingResponse()
                    {
                        Error = "Unable to link role to user",
                        StatusCode = 422,
                        Description = "Role could not be linked to the user.",
                        Email = userEmail,
                        Id = userIdentity.Id,
                        Code = "unable_to_link_role_to_user"
                    });
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
                    new AddRoleErrorResponse()
                    {
                        Role = role,
                        Code = "role_exits",
                        Description = "Role already exists. Please try to add another role.",
                        Error = "Role exists.",
                        StatusCode = 424
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
                    new AddRoleErrorResponse()
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

        // POST api/accounts/addusertorole
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
                        Code = "useremail_or_role_is_empty",
                        Description = "",
                        Error = "User email or role can not be empty",
                        StatusCode = 400
                    });
            }

            bool roleExists = await RoleExists(role);
            bool userExists = await UserExists(id);

            if (!roleExists || !userExists)
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
                    return new JsonResult(Errors.AddUserToRoleErrorResponse(
                        new AddUserToRoleErrorResponse()
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
                return new JsonResult(Errors.AddUserToRoleErrorResponse(
                        new AddUserToRoleErrorResponse()
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

        // PUT api/accounts/removeuserfromrole
        [HttpPut]
        public async Task<IActionResult> RemoveUserFromRole([FromBody] RemoveUserfromRoleViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = model.UserId.Trim();
            var role = model.Role.Trim();

            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(role))
            {
                return new JsonResult(
                    Errors
                    .RemoveRoleFromUserErrorResponse(
                        new RemoveUserfromRoleErrorHAndelingResponse()
                        {
                            Role = role,
                            Email = "no_email",
                            UserId = userId,
                            Code = "user_id_or_ role_is_empty",
                            Description = "User id or role is empty.",
                            Error = "User id  or role can not be empty",
                            StatusCode = 400
                        }));
            }

            var userExists = await UserExists(userId);
            var roleExists = await RoleExists(role);

            if (!userExists || !roleExists)
            {
                return new JsonResult(
                    Errors
                    .RemoveRoleFromUserErrorResponse(
                        new RemoveUserfromRoleErrorHAndelingResponse()
                        {
                            Role = role,
                            Email = "no_email",
                            UserId = userId,
                            Code = "user_or_role__is_not_found",
                            Description = "The user id or the role name does not match a user or a role.",
                            Error = "User or role is not found",
                            StatusCode = 404
                        }));
            }

            var userIdentity = await _userManager.FindByIdAsync(userId);
            var userRole = await _roleManager.FindByNameAsync(role);

            if (await UserHasRole(userIdentity, userRole.Name))
            {
                var removeRoleResult = await _userManager.RemoveFromRoleAsync(userIdentity, userRole.Name);

                if (!removeRoleResult.Succeeded)
                {
                    return new JsonResult(
                        Errors
                        .RemoveRoleFromUserErrorResponse(
                            new RemoveUserfromRoleErrorHAndelingResponse()
                            {
                                Role = role,
                                Email = userIdentity.Email,
                                UserId = userId,
                                Code = "faild_to_remove_role_to_user",
                                Description = "Faild to add role to user.",
                                Error = "Faild to add role to user.",
                                StatusCode = 400
                            }));
                }
            }
            else
            {
                return new JsonResult(
                    Errors
                    .RemoveRoleFromUserErrorResponse(
                        new RemoveUserfromRoleErrorHAndelingResponse()
                        {
                            Role = role,
                            Email = userIdentity.Email,
                            UserId = userId,
                            Code = "user_does_not_have_role_assigned",
                            Description = "User does not have role assigned.",
                            Error = "User does not have role assigned",
                            StatusCode = 400
                        }));
            }

            await _context.SaveChangesAsync();

            return new JsonResult(
                Wrappyfier
                .WrapRemoveUserFromRole(
                    userIdentity.Id,
                    userIdentity.Email,
                    role,
                    200));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRoles([FromQuery] string userId)
        {

            if (String.IsNullOrEmpty(userId))
            {
                return new JsonResult(
                    Errors
                    .GetUserRolesErrorResponse(
                        new GetUserRolesErrorResponse()
                        {
                            Roles = new List<string>(),
                            Code = "userId_can_not_be_empty",
                            Description = "UserId cannot be empty.",
                            Error = "UserId can not be empty",
                            StatusCode = 400,
                            UserId = userId

                        }));
            }

            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                new JsonResult(
                    Errors
                    .GetUserRolesErrorResponse(
                        new GetUserRolesErrorResponse()
                        {
                            Roles = new List<string>(),
                            Code = "user_is_not_found",
                            Description = "User can not be found.",
                            Error = "User is not found",
                            StatusCode = 404,
                            UserId = userId,
                            Email = "no_email"

                        }));
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            return new JsonResult(
                Wrappyfier
                .WrapGetUserRoles(
                    user.Id,
                    user.Email,
                    userRoles.ToList(),
                    200));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string roleId = model.RoleId.Trim();
            string roleName = model.RoleName.Trim();

            if (String.IsNullOrEmpty(roleId) && String.IsNullOrEmpty(roleName))
            {
                return new JsonResult(
                    Errors
                    .DeleteRoleErrorResponse(
                        new DeleteRoleErrorResponse()
                        {
                            RoleName = roleName,
                            RoleId = model.RoleId,
                            Code = "role_id_or_ role_name_is_empty",
                            Description = "Role id or role name is empty",
                            Error = "Role id or role name is empty.",
                            StatusCode = 400
                        }));
            }

            if (!await RoleExists(roleName))
            {
                return new JsonResult(
                  Errors
                  .DeleteRoleErrorResponse(
                      new DeleteRoleErrorResponse()
                      {
                          RoleName = roleName,
                          RoleId = model.RoleId,
                          Code = "role_id_or_role_name_does_not_match_a_role",
                          Description = "Role id or role name does not match a current role",
                          Error = "Role id or role name does not match a role.",
                          StatusCode = 404
                      }));
            }

            IdentityRole roleToDelete = await _roleManager.FindByIdAsync(roleId);
            IList<User> listOfUsersWithCurrentRole = await _userManager.GetUsersInRoleAsync(roleName);

            if (listOfUsersWithCurrentRole.Any())
            {
                return new JsonResult(
                  Errors
                  .DeleteRoleErrorResponse(
                      new DeleteRoleErrorResponse()
                      {
                          RoleName = roleName,
                          RoleId = model.RoleId,
                          Code = "Conflict, role_is_being_used_by_users",
                          Description = "Current role is being used by a user. Please remove dependencies before deleting this role.",
                          Error = "Role is beinging used by users",
                          StatusCode = 409
                      }));
            }

            IdentityResult deleteRoleResult = await _roleManager.DeleteAsync(roleToDelete);

            if (!deleteRoleResult.Succeeded)
            {
                return new JsonResult(
               Errors
               .DeleteRoleErrorResponse(
                   new DeleteRoleErrorResponse()
                   {
                       RoleName = roleName,
                       RoleId = roleId,
                       Code = "unable_to_complete_delete_operation",
                       Description = "Server was unable to delete the role.",
                       Error = "Unable to complete delete operation",
                       StatusCode = 422
                   }));
            }

            await _context.SaveChangesAsync();

            return new JsonResult(
                Wrappyfier
                .WrapDeleteRole(
                    roleToDelete.Id,
                    roleToDelete.Name,
                    200
                    ));
        }


        // Token is sent from gateway server. From "GetAllRoles" method. When Token is not correctly validated the server returns 
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            List<GetAllRoles> roles = new List<GetAllRoles>();
            try
            {
                roles = _roleManager
                  .Roles.Select(
                  x => new GetAllRoles()
                  {
                      Id = x.Id,
                      RoleName = x.Name

                  }).ToList();
            }
            catch (NotSupportedException ex)
            {

                return new JsonResult(
                    Errors
                    .GetAllRolesErrorResponse(
                        new GetAllRolesResponse()
                        {
                            ListOfAllRoles = null,
                            StatusCode = 400,
                            Code = "not_supported_exception",
                            Description = ex.Message.ToString(),
                            Error =  ex.StackTrace.ToString()
                        }));
            }

            return new JsonResult(Wrappyfier.WrapGetAllRolesResponse(roles));
        }
               
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
                    .Format(Constants
                            .APIMessages.NotFoundMessage,
                             model.Id
                             )));
            }

            var rolesForUser = await _userManager.GetRolesAsync(user);

            if (rolesForUser.Any())
            {
                IdentityResult removeLoginsResult = await _userManager.RemoveFromRolesAsync(user, rolesForUser);

                if (!removeLoginsResult.Succeeded)
                {
                    return new JsonResult(
                        Errors
                        .DeleteUserErrorResponse(
                            new DeleteUserErrorMessage()
                            {
                                Email = user.Email,
                                Id = user.Id,
                                Code = "unable_to_complete_delete_operation_of_user_related_roles",
                                StatusCode = 422,
                                Description = "Roles realted to the current user could not be removed at this time.",
                                Error = "Unable to complete delete opretaion of user related roles."
                            }));
                }
            }

            IdentityResult removeUserResult = await _userManager.DeleteAsync(user);
            if (!removeUserResult.Succeeded)
            {
                return new JsonResult(
                    Errors.DeleteUserErrorResponse(
                        new DeleteUserErrorMessage()
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Code = "unable_to_complete_delete_operation",
                            StatusCode = 422,
                            Description = "User was not deleted. The delete task could not be completed at this time.",
                            Error = "Unable to complete delete opretaion."
                        }));
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(Wrappyfier.WrapDeleteUserResponse(user.Id, user.Email));
        }



        //Helper methods
        public async Task<bool> UserHasRole(User user, string role)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            bool userHasRole = userRoles.Any(x => x.Equals(role));

            return userHasRole;
        }

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
    }
}
