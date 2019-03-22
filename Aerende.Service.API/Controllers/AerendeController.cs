using Aerende.Service.API.Helpers;
using Aerende.Service.API.Services;
using Aerende.Service.API.ViewModels.Aerende;
using APIErrorHandling;
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



        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
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

            GetAllPatientJournalsResponse getPatientJournalsResult = await _aerendeService.PostTo<GetAllPatientJournalsResponse>(httpParameters);

            return new JsonResult(getPatientJournalsResult);
        }


        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpGet]
        public async Task<IActionResult> GetPatientJournalById([FromQuery] Guid id, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id == Guid.Empty)
            {
                return new JsonResult(new { error = "error" });
            }

            HttpParameters httpParameters =
               HttpParametersService.GetHttpParameters(
                   null,
                   ConfigHelper.AppSetting(AerendeConstants.ServerUrls, AerendeConstants.GetPatientJournalById),
                   HttpMethod.Get,
                   id.ToString(),
                   authorization
                   );

            GetPatientJournalResponse patientJournalResponse = await _aerendeService.Get<GetPatientJournalResponse>(httpParameters);

            return new JsonResult(patientJournalResponse);
        }
    }
}
