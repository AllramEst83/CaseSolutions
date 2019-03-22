using APIErrorHandling;
using APIResponseMessageWrapper;
using CaseSolutionsTokenValidationParameters.Models;
using Database.Service.API.Services.Interfaces;
using Database.Service.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseModels.DatabaseModels;
using ResponseModels.Models;
using ResponseModels.ViewModels.Aerende;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Service.API.Controllers
{
    [Route("api/database/[controller]/[action]")]
    [ApiController]
    public class AerendeController : ControllerBase
    {
        public AerendeController(IAerendeService aerendeService)
        {
            _aerendeService = aerendeService;
        }

        private IAerendeService _aerendeService { get; }

        [HttpGet]
        public ActionResult<object> Ping()
        {
            return new OkObjectResult(new { ping = "ping" });
        }

        // POST api/database/Aerende/GetAllPatientJournals
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpPost]
        public async Task<IActionResult> GetAllPatientJournals(GetAllPatientJournalslWithCapViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AllPatientJournals patientJournals = await _aerendeService.GetAllPatientJournalsWithCap(model.Cap);
            if (patientJournals.IsNull)
            {
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(
                    new GetAllPatientJournalsResponse()
                    {
                        PatientJournals = patientJournals.PatentJournals,
                        StatusCode = 422,
                        Error = "Get patient journals error",
                        Description = "Unable to return Patient journals",
                        Code = "get_pateint_journals_error"
                    }));
            }

            return new JsonResult(Wrappyfier.WrapPatientJournalsResponse(patientJournals));
        }

        //GET api/database/Aerende/GetPatientJournalById
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpGet]
        public async Task<IActionResult> GetPatientJournalById([FromQuery] Guid id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            if (id == Guid.Empty)
            {
                return new JsonResult(await Errors
                    .GetGenericErrorResponse(
                    new GetPatientJournalResponse()
                    {
                        PatientJournal = new PatientJournal() { Id = id },
                        StatusCode = 400,
                        Error = "Id can not be empty",
                        Description = "Id can not be empty",
                        Code = "id_can_not_be_empty"
                    }));
            }

            PatientJournal patientJournal = await _aerendeService.GetPatientJournalById(id);

            if (patientJournal == null)
            {
                return new JsonResult(await Errors
                 .GetGenericErrorResponse(
                 new GetPatientJournalResponse()
                 {
                     PatientJournal = new PatientJournal() { Id = id },
                     StatusCode = 404,
                     Error = "Patient journal not found",
                     Description = "Patient journal could not be found",
                     Code = "patentJournal_not_found"
                 }));
            }

            return new JsonResult(Wrappyfier.WrapGetPatientJournalResponse(patientJournal));
        }

    }
}
