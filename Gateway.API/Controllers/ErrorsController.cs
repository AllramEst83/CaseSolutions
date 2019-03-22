using Microsoft.AspNetCore.Mvc;
using ResponseModels.ViewModels.Érrors;
using StatusCodeResponseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.API.Controllers
{
    [Route("/errors")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        //Error handeling taken from this post:
        //https://medium.com/@matteocontrini/consistent-error-responses-in-asp-net-core-web-apis-bb70b435d1f8

        [Route("{code}")]
        public IActionResult Error(int code)
        {
            HttpStatusCode parsedCode = (HttpStatusCode)code;
            var genericError = new GenericErrorresponse()
            {
                StatusCode = code,
                Error = parsedCode.ToString(),
                Description = "A error has been thrown on the server. See StatusCode and Error for more information",
                Code = parsedCode.ToString().ToLower()
            };

            return new ObjectResult(genericError);
        }
    }
}
