using Gateway.API.GatewayService;
using Gateway.API.Helpers;
using Gateway.API.Interfaces;
using Gateway.API.ViewModels;
using HttpClientService.Helpers;
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
            HttpParameters httpParameters =
                new HttpParameters
                {
                    Content = model,
                    HttpVerb = HttpMethod.Post,
                    RequestUrl = ConfigHelper.AppSetting(Constants.ServerUrls, Constants.Auth),
                    Id = Guid.Empty,
                    CancellationToken = CancellationToken.None
                };

            var result = await _gWService.Authenticate<object>(httpParameters);

            return new OkObjectResult(result);
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
