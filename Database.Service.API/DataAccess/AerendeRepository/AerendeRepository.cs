using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContext;
using ResponseModels.DatabaseModels;
using System;
using System.Linq;

namespace Database.Service.API.DataAccess.AerendeRepository
{
    public class AerendeRepository
    {
        public AerendeRepository(AerendeContext aerendeContext)
        {
            _aerendeContext = aerendeContext;
        }

        public AerendeContext _aerendeContext { get; }



        public PatientJournal GetPatientJournalById(Guid id)
        {
            PatientJournal patientJournal = _aerendeContext.PatientJournals.SingleOrDefault(x => x.Id == id);

            return patientJournal;
        }

    }




}
