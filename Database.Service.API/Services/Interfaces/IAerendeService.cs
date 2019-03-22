using ResponseModels.DatabaseModels;
using ResponseModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Services.Interfaces
{
    public interface IAerendeService
    {
        Task<AllPatientJournals> GetAllPatientJournalsWithCap(int cap);
        Task<PatientJournal> GetPatientJournalById(Guid guid);
    }
}
