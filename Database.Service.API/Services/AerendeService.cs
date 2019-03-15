using Database.Service.API.DataAccess.AerendeRepository;
using Database.Service.API.Services.Interfaces;
using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Services
{
    public class AerendeService : IAerendeService
    {
        public AerendeService(AerendeRepository aerendeRepository)
        {
            _aerendeRepository = aerendeRepository;
        }

        private AerendeRepository _aerendeRepository { get; }


        public async Task<List<PatientJournal>> GetAllPatientJournalsWithCap(int cap)
        {
            var patientJournals = await _aerendeRepository.GetAllPatientJournalWithCap(cap);

            return patientJournals;
        }

    }
}
