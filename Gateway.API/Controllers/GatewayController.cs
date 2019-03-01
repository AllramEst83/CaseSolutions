using APIErrorHandling;
using APIResponseMessageWrapper;
using CaseSolutionsTokenValidationParameters.Models;
using Gateway.API.Helpers;
using Gateway.API.Interfaces;
using Gateway.API.ViewModels;
using HttpClientService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        //GET api/gateway/CommonAuthTest
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]

        [HttpGet]
        public IActionResult CommonAuthTest()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.CommonAuthTestSuccess));
        }
        //TEST

        // GET api/gateway

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "aerende", "faktura" };
        }

        // GET api/gateway/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/gateway/login
        [AllowAnonymous]
        [HttpPost]
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
            JwtGatewayResponse authResult = await _gWService.PostTo<JwtGatewayResponse>(httpParameters);
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

        // POST api/gateway/signup
        [AllowAnonymous]
        [HttpPost]
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
                    string.Empty);

            SigupGatewayResponse sigUpResult = await _gWService.PostTo<SigupGatewayResponse>(httParameters);
            if (sigUpResult.StatusCode == 400)
            {
                return BadRequest(
                    Errors.
                    AddErrorToModelState(
                        sigUpResult.Code,
                        sigUpResult.Description,
                        ModelState
                        ));
            }
            else if (sigUpResult.StatusCode == 422)
            {
                return new UnprocessableEntityObjectResult(
                   Errors.
                   AddErrorToModelState(
                       sigUpResult.Code,
                       sigUpResult.Description,
                       ModelState
                       ));
            }

            return new OkObjectResult(sigUpResult);
        }

        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model)
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
                    HttpMethod.Post,
                    string.Empty);

            AddUserToRoleGatewayResponse addUserToRoleResult = await _gWService.PostTo<AddUserToRoleGatewayResponse>(httParameters);
            if (addUserToRoleResult.StatusCode == 400)
            {
                return BadRequest(
                     Errors.
                     AddErrorToModelState(
                         addUserToRoleResult.Code,
                         addUserToRoleResult.Description,
                         ModelState
                         ));
            }

            return new OkObjectResult(addUserToRoleResult);
        }

        // POST api/gateway/addrole
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleToAddViewModel model)
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
                string.Empty
                );

            GatewayAddRoleResponse addRoleResult = await _gWService.PostTo<GatewayAddRoleResponse>(httpParameters);
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

        // PUT api/gateway/removeuserfromrole
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpPut]
        public async Task<IActionResult> RemoveUserFromRole(RemoveUserfromRoleViewModel model)
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
                string.Empty
                );

            RemoveUserfromRoleResponseMessage removeUserFromRoleResult =
                await _gWService.PostTo<RemoveUserfromRoleResponseMessage>(httpParameters);

            if (removeUserFromRoleResult.StatusCode == 400)
            {
                return BadRequest(
                   Errors.
                   AddErrorToModelState(
                       removeUserFromRoleResult.Code,
                       removeUserFromRoleResult.Description,
                       ModelState
                       ));
            }
            else if (removeUserFromRoleResult.StatusCode == 404)
            {
                return NotFound(
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

            return new OkObjectResult(removeUserFromRoleResult);
        }

        // GET api/gateway/getuserroles
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpGet]
        public async Task<IActionResult> GetUserRoles(string userId)
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
              userId
              );

            GetUserRolesResponseMessage getUserRolesResult = await _gWService.Get<GetUserRolesResponseMessage>(httpParameters);

            if (getUserRolesResult.StatusCode == 400)
            {
                return BadRequest(Errors
                    .AddErrorToModelState(
                     getUserRolesResult.Code,
                    getUserRolesResult.Description,
                    ModelState
                   ));
            }
            else if (getUserRolesResult.StatusCode == 404)
            {
                return NotFound(Errors
                    .AddErrorToModelState(
                     getUserRolesResult.Code,
                    getUserRolesResult.Description,
                    ModelState
                   ));
            }

            return new OkObjectResult(getUserRolesResult);

        }

        // DELETE api/gateway/deleterole
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(DeleteRoleViewModel model)
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
           string.Empty
           );

            DeleteRoleResponseMessage deleteRoleResult = await _gWService.PostTo<DeleteRoleResponseMessage>(httpParameters);

            if (deleteRoleResult.StatusCode == 400)
            {
                return BadRequest(
                    Errors
                    .AddErrorToModelState(
                        deleteRoleResult.Code,
                        deleteRoleResult.Description,
                        ModelState));
            }

            if (deleteRoleResult.StatusCode == 404)
            {
                return NotFound(
                    Errors
                    .AddErrorToModelState(
                        deleteRoleResult.Code,
                        deleteRoleResult.Description,
                        ModelState));
            }

            if (deleteRoleResult.StatusCode == 409)
            {
                return new ConflictObjectResult(
                    Errors
                    .AddErrorToModelState(
                        deleteRoleResult.Code,
                        deleteRoleResult.Description,
                        ModelState));
            }

            return new OkObjectResult(deleteRoleResult);
        }


        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(DeleteUserViewModel model)
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
                 string.Empty
                 );

            var deleteUserResult = await _gWService.PostTo<DeleteUserResponseMessage>(httpParameters);
            if (deleteUserResult.StatusCode == 404)
            {
                return NotFound(
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

            return new OkObjectResult(deleteUserResult);
        }


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

            var getAllRolesResult = await _gWService.Get<GetAllRolesResponseMessage>(httpParameters);

            if (getAllRolesResult.StatusCode == 400)
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

        // PUT api/gateway/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/gateway/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
