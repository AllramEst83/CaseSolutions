using APIErrorHandling;
using APIResponseMessageWrapper;
using CaseSolutionsTokenValidationParameters.Models;
using Gateway.API.Helpers;
using Gateway.API.Interfaces;
using Gateway.API.ViewModels;
using HttpClientService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseModels.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        public IGWService _gWService { get; }

        public GatewayController(IGWService gWService)
        {
            _gWService = gWService;
        }

        //GET api/gateway/ping
        [HttpGet]
        public IActionResult Ping()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.ZING));
        }

        //TEST
        //GET api/gateway/AdminAuthTest
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpGet]
        public IActionResult AdminAuthTest()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.AdminAuthTestSuccess));
        }

        //GET api/gateway/editAuthTest
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpGet]
        public IActionResult EditAuthTest()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.EditAuthTestSuccess));
        }

        //GET api/gateway/CommonAuthTest
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpGet]
        public IActionResult CommonAuthTest()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.CommonAuthTestSuccess));
        }
        //TEST
                       
        //DONE
        [AllowAnonymous]
        [HttpPost]
        // POST api/gateway/login
        public async Task<IActionResult> LogIn([FromBody] LogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Prepare httpParameters for request
            HttpParameters httpParameters =
                _gWService.GetHttpParameters(
                    model,
                    ConfigHelper.AppSetting(Constants.ServerUrls, Constants.Auth),
                    HttpMethod.Post,
                    string.Empty);

            //httpclient request from class library
            JwtResponse authResult = await _gWService.PostTo<JwtResponse>(httpParameters);
            if (authResult.StatusCode == 400)
            {
                //return user friendly error message
                return BadRequest(
                    Errors
                    .AddErrorToModelState(
                        authResult.Code,
                        authResult.Description,
                        ModelState
                        ));
            }

            //return jwt token
            return new OkObjectResult(authResult);
        }

        //DONE
        [AllowAnonymous]
        [HttpPost]
        // POST api/gateway/signup
        public async Task<IActionResult> SignUp([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            //Prepare httpParameters for request
            HttpParameters httParameters =
                _gWService.GetHttpParameters(
                    model,
                    ConfigHelper.AppSetting(Constants.ServerUrls, Constants.SignUp),
                    HttpMethod.Post,
                    string.Empty
                    );

            SignUpResponse signUpResult = await _gWService.PostTo<SignUpResponse>(httParameters);

            if (signUpResult.StatusCode == 422)
            {
                return new UnprocessableEntityObjectResult(
                   Errors.
                   AddErrorToModelState(
                       signUpResult.Code,
                       signUpResult.Description,
                       ModelState
                       ));
            }
            else if (signUpResult.StatusCode != 200)
            {
                return BadRequest(Errors
                    .AddErrorToModelState(
                     signUpResult.Code,
                    signUpResult.Description,
                    ModelState
                   ));
            }

            return new OkObjectResult(signUpResult);
        }

        //DONE 
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        // POST api/gateway/addrole
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleToAddViewModel model, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpParameters httpParameters =
                _gWService
                .GetHttpParameters
                (model,
                ConfigHelper.AppSetting(Constants.ServerUrls,
                Constants.AddRole),
                HttpMethod.Post,
                string.Empty,
                authorization
                );

            AddRoleResponse addRoleResult = await _gWService.PostTo<AddRoleResponse>(httpParameters);
            if (addRoleResult.StatusCode == 400)
            {
                return BadRequest(
                   Errors.
                   AddErrorToModelState(
                       addRoleResult.Code,
                       addRoleResult.Description,
                       ModelState
                       ));
            }
            //424 - Faild Dependency
            else if (addRoleResult.StatusCode == 424)
            {
                return new ConflictObjectResult(Errors.
                   AddErrorToModelState(
                       addRoleResult.Code,
                       addRoleResult.Description,
                       ModelState
                       ));
            }
            else if (addRoleResult.StatusCode == 422 /*Unprocessable Entity*/)
            {
                return new UnprocessableEntityObjectResult(Errors.
                     AddErrorToModelState(
                         addRoleResult.Code,
                         addRoleResult.Description,
                         ModelState
                         ));
            }

            return new OkObjectResult(addRoleResult);
        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpPut]
        // PUT api/gateway/removeuserfromrole
        public async Task<IActionResult> RemoveUserFromRole(RemoveUserfromRoleViewModel model, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpParameters httpParameters = _gWService
                .GetHttpParameters(
                model,
                ConfigHelper.AppSetting(Constants.ServerUrls, Constants.RemoveUserFromRole),
                HttpMethod.Put,
                string.Empty,
                authorization
                );

            RemoveUserfromRoleResponse removeUserFromRoleResult =
                await _gWService.PostTo<RemoveUserfromRoleResponse>(httpParameters);


            if (removeUserFromRoleResult.StatusCode == 404)
            {
                return new NotFoundObjectResult(
                Errors.
                AddErrorToModelState(
                    removeUserFromRoleResult.Code,
                    removeUserFromRoleResult.Description,
                    ModelState
                    ));
            }
            else if (removeUserFromRoleResult.StatusCode == 422 /*Unprocessable Entity*/)
            {
                return new UnprocessableEntityObjectResult(Errors.
                    AddErrorToModelState(
                        removeUserFromRoleResult.Code,
                        removeUserFromRoleResult.Description,
                        ModelState
                        ));
            }
            else if (removeUserFromRoleResult.StatusCode == 401)
            {
                return Unauthorized(
                    Errors
                    .AddErrorToModelState(
                     removeUserFromRoleResult.Code,
                    removeUserFromRoleResult.Description,
                    ModelState
                   ));
            }
            else if (removeUserFromRoleResult.StatusCode != 200)
            {
                return BadRequest(Errors
                    .AddErrorToModelState(
                     removeUserFromRoleResult.Code,
                    removeUserFromRoleResult.Description,
                    ModelState
                   ));
            }

            return new OkObjectResult(removeUserFromRoleResult);
        }
        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpPut]
        // POST api/gateway/addusertorole
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpParameters httParameters =
                _gWService
                .GetHttpParameters(
                    model,
                    ConfigHelper.AppSetting(Constants.ServerUrls, Constants.AddUserToRole),
                    HttpMethod.Put,
                    string.Empty,
                    authorization
                    );

            AddUserToRoleResponse addUserToRoleResult = await _gWService.PostTo<AddUserToRoleResponse>(httParameters);
            if (addUserToRoleResult.StatusCode == 401)
            {
                return Unauthorized(
                    Errors
                    .AddErrorToModelState(
                     addUserToRoleResult.Code,
                    addUserToRoleResult.Description,
                    ModelState
                   ));
            }
            else if (addUserToRoleResult.StatusCode != 200)
            {
                return BadRequest(Errors
                    .AddErrorToModelState(
                     addUserToRoleResult.Code,
                    addUserToRoleResult.Description,
                    ModelState
                   ));
            }

            return new OkObjectResult(addUserToRoleResult);
        }

        //DONE
        // GET api/gateway/getuserroles
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpGet]
        public async Task<IActionResult> GetUserRoles(string userId, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(userId);
            }

            HttpParameters httpParameters = _gWService
              .GetHttpParameters(
              null,
              ConfigHelper.AppSetting(Constants.ServerUrls, Constants.GetUserRoles),
              HttpMethod.Get,
              userId,
              authorization
              );

            GetUserRolesResponse getUserRolesResult = await _gWService.Get<GetUserRolesResponse>(httpParameters);


            if (getUserRolesResult.StatusCode == 404)
            {
                return new NotFoundObjectResult(Errors
                    .AddErrorToModelState(
                     getUserRolesResult.Code,
                    getUserRolesResult.Description,
                    ModelState
                   ));
            }
            else if (getUserRolesResult.StatusCode == 401)
            {
                return Unauthorized(
                    Errors
                    .AddErrorToModelState(
                     getUserRolesResult.Code,
                    getUserRolesResult.Description,
                    ModelState
                   ));
            }
            else if (getUserRolesResult.StatusCode != 200)
            {
                return BadRequest(Errors
                    .AddErrorToModelState(
                     getUserRolesResult.Code,
                    getUserRolesResult.Description,
                    ModelState
                   ));
            }


            return new OkObjectResult(getUserRolesResult);

        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpDelete]
        // DELETE api/gateway/deleterole
        public async Task<IActionResult> DeleteRole(DeleteRoleViewModel model, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            HttpParameters httpParameters = _gWService
           .GetHttpParameters(
           model,
           ConfigHelper.AppSetting(Constants.ServerUrls, Constants.DeleteRole),
           HttpMethod.Delete,
           string.Empty,
           authorization
           );

            DeleteRoleResponse deleteRoleResult = await _gWService.PostTo<DeleteRoleResponse>(httpParameters);


            if (deleteRoleResult.StatusCode == 404)
            {
                return new NotFoundObjectResult(
                    Errors
                    .AddErrorToModelState(
                        deleteRoleResult.Code,
                        deleteRoleResult.Description,
                        ModelState));
            }
            else if (deleteRoleResult.StatusCode == 409)
            {
                return new ConflictObjectResult(
                    Errors
                    .AddErrorToModelState(
                        deleteRoleResult.Code,
                        deleteRoleResult.Description,
                        ModelState));
            }
            else if (deleteRoleResult.StatusCode == 401)
            {
                return Unauthorized(
                    Errors
                    .AddErrorToModelState(
                        deleteRoleResult.Code,
                        deleteRoleResult.Description,
                        ModelState));
            }
            else if (deleteRoleResult.StatusCode != 200)
            {
                return BadRequest(
                    Errors
                    .AddErrorToModelState(
                        deleteRoleResult.Code,
                        deleteRoleResult.Description,
                        ModelState));
            }

            return new OkObjectResult(deleteRoleResult);
        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(DeleteUserViewModel model, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            HttpParameters httpParameters = _gWService
             .GetHttpParameters(
                 model,
                 ConfigHelper.AppSetting(Constants.ServerUrls, Constants.DeleteUser),
                 HttpMethod.Delete,
                 string.Empty,
                 authorization
                 );

            var deleteUserResult = await _gWService.PostTo<DeleteUserResponse>(httpParameters);

            if (deleteUserResult.StatusCode == 404)
            {
                return new NotFoundObjectResult(
                    Errors
                    .AddErrorToModelState(
                        deleteUserResult.Code,
                        deleteUserResult.Description,
                        ModelState
                        ));
            }
            else if (deleteUserResult.StatusCode == 422)
            {
                return new UnprocessableEntityObjectResult(
                   Errors
                   .AddErrorToModelState(
                       deleteUserResult.Code,
                       deleteUserResult.Description,
                       ModelState
                       ));
            }
            else if (deleteUserResult.StatusCode != 200)
            {
                return BadRequest(
                   Errors
                   .AddErrorToModelState(
                       deleteUserResult.Code,
                       deleteUserResult.Description,
                       ModelState
                       ));

            }

            return new OkObjectResult(deleteUserResult);
        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpGet]
        public async Task<IActionResult> GetAllRoles([FromHeader] string authorization)
        {

            HttpParameters httpParameters = _gWService
               .GetHttpParameters(
                   null,
                   ConfigHelper.AppSetting(Constants.ServerUrls, Constants.GetAllRoles),
                   HttpMethod.Get,
                   string.Empty,
                   authorization
                   );

            var getAllRolesResult = await _gWService.Get<GetAllRolesResponse>(httpParameters);

            if (getAllRolesResult.StatusCode != 200)
            {
                return BadRequest(
                   Errors
                   .AddErrorToModelState(
                       getAllRolesResult.Code,
                       getAllRolesResult.Description,
                       ModelState
                       ));
            }

            return new OkObjectResult(getAllRolesResult);
        }

    }
}
