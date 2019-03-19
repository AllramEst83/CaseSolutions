using CaseSolutionsTokenValidationParameters.Models;
using Gateway.API.Helpers;
using Gateway.API.Interfaces;
using Gateway.API.ViewModels.Aerende;
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

namespace Gateway.API.Controllers
{
    [Route("api/gateway/[controller]/[action]")]
    public class AerendeController : ControllerBase
    {
        public IGWService _gWService { get; }

        public AerendeController(IGWService gWService)
        {
            _gWService = gWService;
        }

        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpPost]
        public async Task<IActionResult> GetAllPatientJournals([FromBody]GetAllPatientJournalslWithCapViewModel model, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpParameters httpParameters =
                HttpParametersService
                .GetHttpParameters(
                    model, 
                    ConfigHelper.AppSetting(Constants.ServerUrls, Constants.GetAllPatientJournals), 
                    HttpMethod.Post,
                    string.Empty,
                    authorization);

            GetAllPatientJournalsResponse getPatientJournalsResult =
                await _gWService.PostTo<GetAllPatientJournalsResponse>(httpParameters);

            return new OkObjectResult(getPatientJournalsResult);
        }
    }
}
