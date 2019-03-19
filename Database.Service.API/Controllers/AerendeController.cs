using CaseSolutionsTokenValidationParameters.Models;
using Database.Service.API.Services.Interfaces;
using Database.Service.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseModels.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Service.API.Controllers
{
    [Route("api/database[controller]/[action]")]
    [ApiController]
    public class AerendeController : ControllerBase
    {
        public AerendeController(IAerendeService aerendeService)
        {
            _aerendeService = aerendeService;
        }

        private IAerendeService _aerendeService { get; }

        // GET api/Get
        [Authorize(Policy = TokenValidationConstants.Policies.AuthAPICommonUser)]
        [HttpPost]
        public async Task<ActionResult<List<PatientJournal>>> GetAllPatientJournals(GetAllPatientJournalslWithCapViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var patientJournals = await _aerendeService.GetAllPatientJournalsWithCap(model.Cap);

            return patientJournals;
        }

    }
}
