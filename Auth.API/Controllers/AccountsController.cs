using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIErrorHandling;
using APIResponseMessageWrapper;
using Auth.API.Helpers;
using Auth.API.Interfaces;
using Auth.API.Models;
using Auth.API.Services;
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
        //private readonly UserManager<User> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IAccountsService _accountsService;

        public AccountsController(
            IAccountsService accountsService,
            IMapper mapper,
            UserContext appDbContext)
        {
            _mapper = mapper;
            _context = appDbContext;
            _accountsService = accountsService;
        }

        // GET api/accounts/ping
        [HttpGet]
        public ActionResult<object> Ping()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.APIMessages.Ping));
        }

        //DONE
        [HttpPost]
        // POST api/accounts/signup
        public async Task<IActionResult> Signup([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = model.Email.Trim();
            if (await _accountsService.UserExists(userEmail))
            {

                return new JsonResult(await Errors.GetGenericErrorResponse(
                    new SignUpResponse()
                    {
                        Id = "no_id",
                        Email = userEmail,
                        StatusCode = 400,
                        Error = "User exists",
                        Description = "Please enter a new user email. This user does already exists.",
                        Code = "user_exists"
                    }));
            }

            var userRole = model.Role.Trim();
            if (!await _accountsService.RoleExists(userRole))
            {
                return new JsonResult(await Errors.GetGenericErrorResponse(
                    new SignUpResponse()
                    {
                        Id = "no_id",
                        Email = userEmail,
                        StatusCode = 400,
                        Error = "Role does not exists",
                        Description = "The role you are trying to link to a user does not exist.",
                        Code = "role_does_not_exists"
                    }));
            }

            var userIdentity = _mapper.Map<User>(model);

            IdentityResult result = await _accountsService.CreateUser(userIdentity, model.Password);
            if (!result.Succeeded)
            {
                return new JsonResult(await Errors.GetGenericErrorResponse(
                    new SignUpResponse()
                    {
                        Error = "Unable to create user",
                        StatusCode = 422,
                        Description = "User could not be created at this time",
                        Email = userEmail,
                        Id = userIdentity.Id ?? "no_id",
                        Code = "unable_to_create_user"
                    }));
            }

            IdentityResult addRoleResult = await _accountsService.AddRoleToUser(userIdentity, userRole);

            if (!addRoleResult.Succeeded)
            {
                return new JsonResult(await Errors.GetGenericErrorResponse(
                    new SignUpResponse()
                    {
                        Error = "Unable to link role to user",
                        StatusCode = 422,
                        Description = "Role could not be linked to the user.",
                        Email = userEmail,
                        Id = userIdentity.Id,
                        Code = "unable_to_link_role_to_user"
                    }));
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(Wrappyfier.WrapSigupResponse(userIdentity.Id, userIdentity.Email, 200));
        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpPost]
        // POST api/accounts/addrole
        public async Task<IActionResult> AddRole([FromBody] RoleToAddViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string role = model.RoleToAdd.Trim();
            if (await _accountsService.RoleExists(role))
            {
                return new JsonResult(await Errors.GetGenericErrorResponse(
                    new AddRoleResponse()
                    {
                        Role = role,
                        Code = "role_exits",
                        Description = "Role already exists. Please try to add another role.",
                        Error = "Role exists.",
                        StatusCode = 424
                    }));
            }

            IdentityResult roleResult = await _accountsService.CreateRole(role);

            if (!roleResult.Succeeded)
            {
                return new JsonResult(await Errors.GetGenericErrorResponse(
                    new AddRoleResponse()
                    {
                        Id = "no_id",
                        Role = role,
                        Code = "faild_to_add_role",
                        Description = "Faild to add role to database.",
                        Error = "Faild to add role.",
                        StatusCode = 424
                    }));
            }

            await _context.SaveChangesAsync();

            IdentityRole identityRole = await _accountsService.GetRoleId(role);

            return new OkObjectResult(Wrappyfier.WrapAddRoleResponse(identityRole.Id, identityRole.Name));
        }

        //DONE
        [HttpPut]
        // POST api/accounts/addusertorole
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
                return new JsonResult(await Errors.GetGenericErrorResponse(
                    new AddUserToRoleResponse()
                    {
                        UserId = model.Id,
                        Role = role,
                        Email = "no_email",
                        StatusCode = 400,
                        Error = "User email or role can not be empty",
                        Description = "",
                        Code = "useremail_or_role_is_empty"
                    }));
            }

            bool roleExists = await _accountsService.RoleExists(role);
            bool userExists = await _accountsService.UserExists(id);

            if (!roleExists || !userExists)
            {
                return new JsonResult(await Errors.GetGenericErrorResponse(
                      new AddUserToRoleResponse()
                      {
                          UserId = model.Id,
                          Role = role,
                          Email = "no_email",
                          StatusCode = 404,
                          Error = "User or role is not found",
                          Description = "The user id or the role name does not match a user or a role.",
                          Code = "user_or_role__is_not_found"

                      }));
            }

            User userIdentity = await _accountsService.GetUser(id);
            IdentityRole userRole = await _accountsService.GetRoleByName(role);

            if (!await _accountsService.UserHasRole(userIdentity, userRole.Name))
            {
                IdentityResult addRoleResult = await _accountsService.AddRoleToUser(userIdentity, userRole.Name);

                if (!addRoleResult.Succeeded)
                {
                    return new JsonResult(await Errors.GetGenericErrorResponse(
                        new AddUserToRoleResponse()
                        {
                            UserId = userIdentity.Id,
                            Role = userRole.Name,
                            StatusCode = 424,
                            Error = "Faild to add role to user.",
                            Description = "Faild to add role to user.",
                            Code = "faild_to_add_role_to_user",
                        }));
                }
            }
            else
            {
                return new JsonResult(await Errors.GetGenericErrorResponse(
                        new AddUserToRoleResponse()
                        {
                            UserId = userIdentity.Id,
                            Role = userRole.Name,
                            StatusCode = 400,
                            Error = "User has already role assigned.",
                            Description = "User already has the role assigned",
                            Code = "user_has_already_role_assigned",
                        }));
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(Wrappyfier.WrapAddRoleToUserResponse(userIdentity.Email, userRole.Name));
        }

        //DONE       
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpPut]
        //PUT api/accounts/removeuserfromrole
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
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(
                        new AddUserToRoleResponse()
                        {
                            UserId = userId,
                            Email = "no_email",
                            Role = role,
                            StatusCode = 400,
                            Error = "User id  or role can not be empty",
                            Description = "User id or role is empty.",
                            Code = "user_id_or_ role_is_empty"
                        }));
            }

            var userExists = await _accountsService.UserExists(userId);
            var roleExists = await _accountsService.RoleExists(role);

            if (!userExists || !roleExists)
            {
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(
                        new AddUserToRoleResponse()
                        {
                            UserId = userId,
                            Email = "no_email",
                            Role = role,
                            StatusCode = 404,
                            Error = "User or role is not found",
                            Description = "The user id or the role name does not match a user or a role.",
                            Code = "user_or_role__is_not_found"
                        }));
            }

            User userIdentity = await _accountsService.GetUser(userId);
            IdentityRole userRole = await _accountsService.GetRoleByName(role);

            if (await _accountsService.UserHasRole(userIdentity, userRole.Name))
            {
                IdentityResult removeRoleResult = await _accountsService.RemoveRolefromUser(userIdentity, userRole.Name);

                if (!removeRoleResult.Succeeded)
                {
                    return new JsonResult(await Errors
                        .GetGenericErrorResponse(
                            new AddUserToRoleResponse()
                            {
                                UserId = userIdentity.Id,
                                Email = userIdentity.Email,
                                Role = userRole.Name,
                                StatusCode = 400,
                                Error = "Faild to add role to user.",
                                Description = "Faild to add role to user.",
                                Code = "faild_to_remove_role_to_user",
                            }));
                }
            }
            else
            {
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(
                        new AddUserToRoleResponse()
                        {
                            UserId = userIdentity.Id,
                            Email = userIdentity.Email,
                            Role = userRole.Name,
                            StatusCode = 400,
                            Error = "User does not have role assigned",
                            Description = "User does not have role assigned.",
                            Code = "user_does_not_have_role_assigned"
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

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpGet]
        //GET/api/auth/getuserroles
        public async Task<IActionResult> GetUserRoles([FromQuery] string userId)
        {
            //HERE -> CustomException skapar inte en user klass när user är null
            if (String.IsNullOrEmpty(userId))
            {
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(
                        new GetUserRolesResponse()
                        {
                            Email = "no_email",
                            Roles = new List<string>(),
                            UserId = userId,
                            StatusCode = 400,
                            Error = "UserId can not be empty",
                            Description = "UserId cannot be empty.",
                            Code = "userId_can_not_be_empty"
                        }));
            }

            User user = await _accountsService.GetUser(userId);

            if (user == null)
            {
                return new JsonResult(await Errors
                      .GetGenericErrorResponse(
                          new GetUserRolesResponse()
                          {
                              Email = "no_email",
                              Roles = new List<string>(),
                              UserId = userId,
                              StatusCode = 404,
                              Error = "User is not found",
                              Description = "User can not be found.",
                              Code = "user_is_not_found"
                          }));
            }

            RolesForUser userRoles = await _accountsService.GetUserRoles(user);

            return new JsonResult(
                Wrappyfier
                .WrapGetUserRoles(
                    user.Id,
                    user.Email,
                    userRoles.Roles.ToList(),
                    200));
        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpDelete]
        //Delete  /api/auth/deleterole
        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string roleId = model.RoleId.Trim();
            string roleName = model.RoleName.Trim();

            if (String.IsNullOrEmpty(roleId) || String.IsNullOrEmpty(roleName))
            {
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(
                        new DeleteRoleResponse()
                        {
                            RoleName = "no_id",
                            RoleId = "no_role_name",
                            StatusCode = 400,
                            Error = "Role id or role name is empty.",
                            Description = "Role id or role name is empty",
                            Code = "role_id_or_role_name_is_empty",
                        }));
            }

            if (!await _accountsService.RoleExists(roleName))
            {
                return new JsonResult(await Errors
                  .GetGenericErrorResponse(
                      new DeleteRoleResponse()
                      {
                          RoleName = roleName,
                          RoleId = model.RoleId,
                          StatusCode = 404,
                          Error = "Role id or role name does not match a role.",
                          Description = "Role id or role name does not match a current role",
                          Code = "role_id_or_role_name_does_not_match_a_role",

                      }));
            }

            UsersInRole listOfUsersWithCurrentRole = await _accountsService.GetUsersInRole(roleName);

            if (listOfUsersWithCurrentRole.IsNull)
            {
                return new JsonResult(await Errors
                        .GetGenericErrorResponse(
                            new DeleteUserResponse()
                            {
                                Id = "no_id",
                                Email = "no_email",
                                StatusCode = 422,
                                Error = "Get users in role error",
                                Description = "Unable to get users in role.",
                                Code = "get_users_in_role_error",
                            }));
            }

            if (listOfUsersWithCurrentRole.User.Any())
            {
                return new JsonResult(
                 await Errors
                  .GetGenericErrorResponse(
                      new DeleteRoleResponse()
                      {
                          RoleName = roleName,
                          RoleId = model.RoleId,
                          StatusCode = 409,
                          Error = "Role is beinging used by users",
                          Description = "Current role is being used by a user. Please remove dependencies before deleting this role.",
                          Code = "Conflict, role_is_being_used_by_users"
                      }));
            }

            IdentityRole roleToDelete = await _accountsService.GetRoleByName(roleName);

            IdentityResult deleteRoleResult = await _accountsService.DeleteRole(roleToDelete);

            if (!deleteRoleResult.Succeeded)
            {
                return new JsonResult(await Errors
               .GetGenericErrorResponse(
                   new DeleteRoleResponse()
                   {
                       RoleName = roleName,
                       RoleId = roleId,
                       StatusCode = 422,
                       Error = "Unable to complete delete operation",
                       Description = "Server was unable to delete the role.",
                       Code = "unable_to_complete_delete_operation"

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

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpGet]
        //GET  /api/auth/getallroles
        public async Task<IActionResult> GetAllRoles()
        {
            AllRoles roles = await _accountsService.GetAllRoles();

            if (roles.IsNull)
            {
                return new JsonResult(await Errors
                  .GetGenericErrorResponse(
                      new GetAllRolesResponse()
                      {
                          ListOfAllRoles = null,
                          StatusCode = 400,
                          Error = "Server was unable to get a list of roles",
                          Description = "The server could not get the list of roles.",
                          Code = "server_was_unable_to_a_get_list_of_roles"
                      }));
            }

            return new JsonResult(Wrappyfier.WrapGetAllRolesResponse(roles._AllRoles));
        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpDelete]
        //Delete  /api/auth/deleteuser
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _accountsService.GetUser(model.Id);

            if (user == null)
            {
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(
                    new DeleteUserResponse()
                    {
                        Id = model.Id,
                        Email = "no_email",
                        StatusCode = 401,
                        Error = "User not found",
                        Description = String.Format(Constants.APIMessages.NotFoundMessage, model.Id),
                        Code = "user_not_found",
                    }));
            }


            RolesForUser rolesForUser = await _accountsService.GetUserRoles(user);

            if (rolesForUser.IsNull)
            {
                return new JsonResult(await Errors
                        .GetGenericErrorResponse(
                            new DeleteUserResponse()
                            {
                                Id = "no_id",
                                Email = "no_email",
                                StatusCode = 422,
                                Error = "Get roles for user error",
                                Description = "Unable to get roles for user.",
                                Code = "get_roles_for_user_error",
                            }));
            }

            if (rolesForUser.Roles.Any())
            {
                IdentityResult removeRolesFromUserResult = await _accountsService.RemoveRolesFromUser(user, rolesForUser.Roles);

                if (!removeRolesFromUserResult.Succeeded)
                {
                    return new JsonResult(await Errors
                        .GetGenericErrorResponse(
                            new DeleteUserResponse()
                            {
                                Id = user.Id,
                                Email = user.Email,
                                StatusCode = 422,
                                Error = "Unable to complete delete opretaion of user related roles.",
                                Description = "Roles realted to the current user could not be removed at this time.",
                                Code = "unable_to_complete_delete_operation_of_user_related_roles",
                            }));
                }
            }

            IdentityResult removeUserResult = await _accountsService.DeleteUser(user);

            if (!removeUserResult.Succeeded)
            {
                IdentityResult reAddRolesToUserResult = await _accountsService.AddRolesToUser(user, rolesForUser.Roles);

                if (reAddRolesToUserResult.Succeeded)
                {
                    return new JsonResult(await Errors.GetGenericErrorResponse(
                        new DeleteUserResponse()
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Code = "unable_to_complete_delete_operation",
                            StatusCode = 422,
                            Description = "User was not deleted. The delete task could not be completed at this time.",
                            Error = "Unable to complete delete opretaion."
                        }));
                }

                return new JsonResult(await Errors.GetGenericErrorResponse(
                  new DeleteUserResponse()
                  {
                      Id = user.Id,
                      Email = user.Email,
                      Code = "unable_to_complete_delete_operation_USER_DOES_NOT_HAVE_ANY_ROLES",
                      StatusCode = 422,
                      Description = "User was not deleted. The delete task could not be completed at this time. User has no roles assign. Pleas add roles to user for access.",
                      Error = "Unable to complete delete opretaion. User does not have any roles"
                  }));
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(Wrappyfier.WrapDeleteUserResponse(user.Id, user.Email));
        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpGet]
        public async Task<IActionResult> GetListOfUsers()
        {
            AllUsers users = await _accountsService.GetListOfUsers();

            if (users.IsNull)
            {
                return new JsonResult(await Errors
              .GetGenericErrorResponse(
                  new GetAllUsersResponse()
                  {
                      ListOfAllUsers = null,
                      StatusCode = 400,
                      Error = "Server was unable to get a list of users",
                      Description = "The server could not get the list of users.",
                      Code = "server_was_unable_to_a_get_list_of_users"
                  }));
            }

            return new JsonResult(
                Wrappyfier.WrapAPIList(
                    200,
                    users._AllUsers.Count == 0 ?
                    Constants.APIMessages.ListOfUsersEmpty :
                    Constants.APIMessages.ListOfUsers,
                    users._AllUsers

                    ));
        }
    }
}
