using APIErrorHandling;
using APIResponseMessageWrapper;
using CaseSolutionsTokenValidationParameters.Models;
using Gateway.API.Helpers;
using Gateway.API.Interfaces;
using Gateway.API.ViewModels;
using HttpClientService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            HttpParameters httpParameters = _gWService.GetHttpParameters(model, ConfigHelper.AppSetting(Constants.ServerUrls, Constants.Auth));

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
            HttpParameters httParameters = _gWService.GetHttpParameters(model, ConfigHelper.AppSetting(Constants.ServerUrls, Constants.SignUp));

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

            return new OkObjectResult(sigUpResult);
        }

        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpParameters httParameters = _gWService.GetHttpParameters(model, ConfigHelper.AppSetting(Constants.ServerUrls, Constants.AddUserToRole));

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
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPIAdmin)]
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleToAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpParameters httpParameters = _gWService.GetHttpParameters(model, ConfigHelper.AppSetting(Constants.ServerUrls, Constants.AddRole));

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
                new UnprocessableEntityObjectResult(Errors.
                   AddErrorToModelState(
                       addRoleResult.Code,
                       addRoleResult.Description,
                       ModelState
                       ));
            }

            return new OkObjectResult(addRoleResult);
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
