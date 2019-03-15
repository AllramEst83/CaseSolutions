using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.DataAccess.AerendeRepository
{
    interface IAerendeRepository
    {
        Task<List<PatientJournal>> GetAllPatientJournalWithCap(int cap);
        Task<PatientJournal> GetPatientJournalById(Guid id);
    }
}
