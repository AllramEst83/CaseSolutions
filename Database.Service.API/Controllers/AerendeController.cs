using Database.Service.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseModels.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AerendeController : ControllerBase
    {
        public AerendeController(IAerendeService aerendeService)
        {
            _aerendeService = aerendeService;
        }

        private IAerendeService _aerendeService { get; }

        // GET api/Get
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<PatientJournal>>> GetAllPatientJournalslWithCap()
        {
            var patientJournals = await _aerendeService.GetAllPatientJournalsWithCap(20);

            return patientJournals;
        }

    }
}
