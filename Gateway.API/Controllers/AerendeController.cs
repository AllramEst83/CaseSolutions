using CaseSolutionsTokenValidationParameters.Models;
using Gateway.API.Helpers;
using Gateway.API.Interfaces;
using Gateway.API.ViewModels.Aerende;
using HttpClientService;
using HttpClientService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseModels.DatabaseModels;
using ResponseModels.ViewModels.Aerende;
using StatusCodeResponseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.API.Controllers
{
    [Route("api/gateway/[controller]/[action]")]
    [ApiController]
    public class AerendeController : ControllerBase
    {
        public IGWService GWService { get; }

        public AerendeController(IGWService gWService)
        {
            GWService = gWService;

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
               await GWService.PostTo<GetAllPatientJournalsResponse>(httpParameters);


            if (getPatientJournalsResult.StatusCode == 422)
            {
                return await ResponseService
                    .GetResponse<UnprocessableEntityObjectResult, GetAllPatientJournalsResponse>(getPatientJournalsResult, ModelState);
            }

            return new OkObjectResult(getPatientJournalsResult);
        }

        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpGet]
        public async Task<IActionResult> GetPatientJournalById(Guid id, [FromHeader]string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == Guid.Empty)
            {
                return BadRequest("id can not be empty");
            }

            HttpParameters httpParameters =
           HttpParametersService.GetHttpParameters(
               null,
               ConfigHelper.AppSetting(Constants.ServerUrls, Constants.GetPatientJournalById),
               HttpMethod.Get,
               id.ToString(),
               authorization
               );

            GetPatientJournalResponse patientJournalResult = await GWService.Get<GetPatientJournalResponse>(httpParameters);

            if (patientJournalResult.StatusCode == 400)
            {
                return await ResponseService.GetResponse<BadRequestObjectResult, GetPatientJournalResponse>(patientJournalResult, ModelState);
            }
            else if (patientJournalResult.StatusCode == 404)
            {
                return await ResponseService.GetResponse<NotFoundObjectResult, GetPatientJournalResponse>(patientJournalResult, ModelState);
            }

            return new OkObjectResult(patientJournalResult);
        }


    }
}
