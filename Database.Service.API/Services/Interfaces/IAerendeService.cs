using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Services.Interfaces
{
    public interface IAerendeService
    {
        Task<List<PatientJournal>> GetAllPatientJournalsWithCap(int cap);
    }
}
