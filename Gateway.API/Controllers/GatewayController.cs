using Gateway.API.GatewayService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        public GWService _gWService { get; }

        public GatewayController(GWService gWService)
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

        // POST api/gateway
        [HttpPost]
        public void Post([FromBody] string value)
        {

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
