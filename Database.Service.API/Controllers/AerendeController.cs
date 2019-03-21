using CaseSolutionsTokenValidationParameters.Models;
using Database.Service.API.Services.Interfaces;
using Database.Service.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseModels.DatabaseModels;
using ResponseModels.ViewModels.Aerende;
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

        // GET api/Get
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpPost]
        public async Task<IActionResult> GetAllPatientJournals(GetAllPatientJournalslWithCapViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var patientJournals = await _aerendeService.GetAllPatientJournalsWithCap(model.Cap);

            GetAllPatientJournalsResponse patientJournalsResponse =
                new GetAllPatientJournalsResponse()
                {
                    PatientJournals = patientJournals,
                    StatusCode = 200,
                    Error = "No error",
                    Description = "Thi is all patient journals. If no cap was supplyed all patient journals are returned",
                    Code = "no_error"
                };

            return new OkObjectResult(patientJournalsResponse); ;
        }

    }
}
