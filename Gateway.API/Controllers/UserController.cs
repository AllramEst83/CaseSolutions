using APIErrorHandling;
using APIResponseMessageWrapper;
using CaseSolutionsTokenValidationParameters.Models;
using Gateway.API.Helpers;
using Gateway.API.Interfaces;
using Gateway.API.ViewModels;
using HttpClientService;
using HttpClientService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseModels.ViewModels;
using StatusCodeResponseService;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.API.Controllers
{
    [Route("api/gateway/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IGWService GwService { get; }

        public UserController(IGWService gwService)
        {
            GwService = gwService;
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
                HttpParametersService.GetHttpParameters(
                    model,
                    ConfigHelper.AppSetting(Constants.ServerUrls, Constants.Auth),
                    HttpMethod.Post,
                    string.Empty);

            //httpclient request from class library
            JwtResponse authResult = await GwService.PostTo<JwtResponse>(httpParameters);

            if (authResult.StatusCode == 400)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, JwtResponse>(authResult, ModelState);
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
                HttpParametersService.GetHttpParameters(
                    model,
                    ConfigHelper.AppSetting(Constants.ServerUrls, Constants.SignUp),
                    HttpMethod.Post,
                    string.Empty
                    );

            SignUpResponse signUpResult = await GwService.PostTo<SignUpResponse>(httParameters);

            if (signUpResult.StatusCode == 422)
            {
                return await ResponseService.GetResponse<UnprocessableEntityObjectResult, SignUpResponse>(signUpResult, ModelState);
            }
            else if (signUpResult.StatusCode != 200)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, SignUpResponse>(signUpResult, ModelState);
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
                HttpParametersService
                .GetHttpParameters
                (model,
                ConfigHelper.AppSetting(Constants.ServerUrls,
                Constants.AddRole),
                HttpMethod.Post,
                string.Empty,
                authorization
                );

            AddRoleResponse addRoleResult = await GwService.PostTo<AddRoleResponse>(httpParameters);

            if (addRoleResult.StatusCode == 400)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, AddRoleResponse>(addRoleResult, ModelState);
            }
            else if (addRoleResult.StatusCode == 424)
            {
                return await ResponseService.GetResponse<ConflictObjectResult, AddRoleResponse>(addRoleResult, ModelState);
            }
            else if (addRoleResult.StatusCode == 422)
            {
                return await ResponseService.GetResponse<UnprocessableEntityObjectResult, AddRoleResponse>(addRoleResult, ModelState);
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

            HttpParameters httpParameters = HttpParametersService
                .GetHttpParameters(
                model,
                ConfigHelper.AppSetting(Constants.ServerUrls, Constants.RemoveUserFromRole),
                HttpMethod.Put,
                string.Empty,
                authorization
                );

            RemoveUserfromRoleResponse removeUserFromRoleResult =
                await GwService.PostTo<RemoveUserfromRoleResponse>(httpParameters);

            if (removeUserFromRoleResult.StatusCode == 404)
            {
                return await
                    ResponseService.GetResponse<NotFoundObjectResult, RemoveUserfromRoleResponse>(removeUserFromRoleResult, ModelState);
            }
            else if (removeUserFromRoleResult.StatusCode == 422)
            {
                return await ResponseService.GetResponse<UnprocessableEntityObjectResult, RemoveUserfromRoleResponse>(removeUserFromRoleResult, ModelState);
            }
            else if (removeUserFromRoleResult.StatusCode == 401)
            {
                return await ResponseService.GetResponse<UnauthorizedObjectResult,RemoveUserfromRoleResponse>(removeUserFromRoleResult, ModelState);
            }
            else if (removeUserFromRoleResult.StatusCode != 200)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, RemoveUserfromRoleResponse>(removeUserFromRoleResult, ModelState);
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
                HttpParametersService
                .GetHttpParameters(
                    model,
                    ConfigHelper.AppSetting(Constants.ServerUrls, Constants.AddUserToRole),
                    HttpMethod.Put,
                    string.Empty,
                    authorization
                    );

            AddUserToRoleResponse addUserToRoleResult = await GwService.PostTo<AddUserToRoleResponse>(httParameters);
            if (addUserToRoleResult.StatusCode == 401)
            {
                return await ResponseService.GetResponse<UnauthorizedObjectResult,AddUserToRoleResponse>(addUserToRoleResult, ModelState);
            }
            else if (addUserToRoleResult.StatusCode != 200)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, AddUserToRoleResponse>(addUserToRoleResult, ModelState);
            }

            return new OkObjectResult(addUserToRoleResult);
        }

        //DONE
        // GET api/gateway/getuserroles
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpGet]
        public async Task<IActionResult> GetUserRoles(string id, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(id);
            }

            HttpParameters httpParameters = HttpParametersService
              .GetHttpParameters(
              null,
              ConfigHelper.AppSetting(Constants.ServerUrls, Constants.GetUserRoles),
              HttpMethod.Get,
              id,
              authorization
              );

            GetUserRolesResponse getUserRolesResult = await GwService.Get<GetUserRolesResponse>(httpParameters);


            if (getUserRolesResult.StatusCode == 404)
            {
                return await ResponseService.GetResponse<NotFoundObjectResult, GetUserRolesResponse>(getUserRolesResult, ModelState);
            }
            else if (getUserRolesResult.StatusCode == 401)
            {
                return await ResponseService.GetResponse<UnauthorizedObjectResult, GetUserRolesResponse>(getUserRolesResult, ModelState);
            }
            else if (getUserRolesResult.StatusCode != 200)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, GetUserRolesResponse>(getUserRolesResult, ModelState);
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

            HttpParameters httpParameters = HttpParametersService
           .GetHttpParameters(
           model,
           ConfigHelper.AppSetting(Constants.ServerUrls, Constants.DeleteRole),
           HttpMethod.Delete,
           string.Empty,
           authorization
           );

            DeleteRoleResponse deleteRoleResult = await GwService.PostTo<DeleteRoleResponse>(httpParameters);


            if (deleteRoleResult.StatusCode == 404)
            {
                return await ResponseService.GetResponse<NotFoundObjectResult, DeleteRoleResponse>(deleteRoleResult, ModelState);
            }
            else if (deleteRoleResult.StatusCode == 409)
            {
                return await ResponseService.GetResponse<ConflictObjectResult, DeleteRoleResponse>(deleteRoleResult, ModelState);
            }
            else if (deleteRoleResult.StatusCode == 401)
            {
                return await ResponseService.GetResponse<UnauthorizedObjectResult, DeleteRoleResponse>(deleteRoleResult, ModelState);
            }
            else if (deleteRoleResult.StatusCode != 200)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, DeleteRoleResponse>(deleteRoleResult, ModelState);
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

            HttpParameters httpParameters = HttpParametersService
             .GetHttpParameters(
                 model,
                 ConfigHelper.AppSetting(Constants.ServerUrls, Constants.DeleteUser),
                 HttpMethod.Delete,
                 string.Empty,
                 authorization
                 );

            DeleteUserResponse deleteUserResult = await GwService.PostTo<DeleteUserResponse>(httpParameters);

            if (deleteUserResult.StatusCode == 404)
            {
                return await ResponseService.GetResponse<NotFoundObjectResult, DeleteUserResponse>(deleteUserResult, ModelState);
            }
            else if (deleteUserResult.StatusCode == 422)
            {
                return await ResponseService.GetResponse<UnprocessableEntityObjectResult, DeleteUserResponse>(deleteUserResult, ModelState);
            }
            else if (deleteUserResult.StatusCode != 200)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, DeleteUserResponse>(deleteUserResult, ModelState);

            }

            return new OkObjectResult(deleteUserResult);
        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpGet]
        public async Task<IActionResult> GetAllRoles([FromHeader] string authorization)
        {

            HttpParameters httpParameters = HttpParametersService
               .GetHttpParameters(
                   null,
                   ConfigHelper.AppSetting(Constants.ServerUrls, Constants.GetAllRoles),
                   HttpMethod.Get,
                   string.Empty,
                   authorization
                   );

            GetAllRolesResponse getAllRolesResult = await GwService.Get<GetAllRolesResponse>(httpParameters);

            if (getAllRolesResult.StatusCode != 200)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, GetAllRolesResponse>(getAllRolesResult, ModelState);
            }

            return new OkObjectResult(getAllRolesResult);
        }

        //DONE
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIEditUser)]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromHeader] string authorization)
        {

            HttpParameters httpParameters = HttpParametersService
               .GetHttpParameters(
                   null,
                   ConfigHelper.AppSetting(Constants.ServerUrls, Constants.GetAllUsers),
                   HttpMethod.Get,
                   string.Empty,
                   authorization
                   );

            GetAllUsersResponse getAllRolesResult = await GwService.Get<GetAllUsersResponse>(httpParameters);

            if (getAllRolesResult.StatusCode != 200)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, GetAllUsersResponse>(getAllRolesResult, ModelState);
            }

            return new OkObjectResult(getAllRolesResult);
        }

    }
}
