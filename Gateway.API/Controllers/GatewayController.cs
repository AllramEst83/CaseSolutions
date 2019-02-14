using APIErrorHandling;
using APIResponseMessageWrapper;
using Gateway.API.GatewayService;
using Gateway.API.Helpers;
using Gateway.API.Interfaces;
using Gateway.API.ViewModels;
using HttpClientService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
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


        [HttpGet]
        public IActionResult Ping()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.ZING));
        }

        [Authorize(Policy = Constants.GatewayAPIAdmin)]
        [HttpGet]
        public IActionResult AuthTest()
        {
            return new OkObjectResult(Wrappyfier.WrapResponse(200, Constants.AuthTestSuccess));
        }

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
        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] LogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Prepare httpParameters for request
            HttpParameters httpParameters = _gWService.GetHttpParameters(model);

            //httpclient request from class library
            var authResult = await _gWService.Authenticate<JwtGatewayResponse>(httpParameters);
            if (authResult.StatusCode == 400)
            {
                //return user friendly error message
                return BadRequest(Errors.AddErrorToModelState(authResult.Code, authResult.Description, ModelState));
            }

            //return jwt token
            return new OkObjectResult(authResult);
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
