using Aerende.Service.API.Helpers;
using Aerende.Service.API.Services;
using Aerende.Service.API.ViewModels.Aerende;
using CaseSolutionsTokenValidationParameters.Models;
using HttpClientService;
using HttpClientService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseModels.ViewModels.Aerende;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aerende.Service.API.Controllers
{
    [Route("api/aerende/[controller]/[action]")]
    [ApiController]
    public class AerendeController : ControllerBase
    {
        public IAerendeService _aerendeService { get; }

        public AerendeController(IAerendeService aerendeService)
        {
            _aerendeService = aerendeService;
        }



        //[Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpPost]
        public async Task<IActionResult> GetAllPatientJournals(GetAllPatientJournalslWithCapViewModel model, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpParameters httpParameters =
                HttpParametersService.GetHttpParameters(
                    model,
                    ConfigHelper.AppSetting(AerendeConstants.ServerUrls, AerendeConstants.PatientJournals),
                    HttpMethod.Post,
                    string.Empty,
                    authorization
                    );

            GetAllPatientJournalsResponse getPatientJournalsResult = await _aerendeService.Get<GetAllPatientJournalsResponse>(httpParameters);

            return new OkObjectResult(new { });
        }
    }
}
