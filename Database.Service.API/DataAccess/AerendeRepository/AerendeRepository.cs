using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContextFolder;
using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;//<-Gör så att man kan använda "Include"
using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeExtensions;

namespace Database.Service.API.DataAccess.AerendeRepository
{
    public class AerendeRepository : IAerendeRepository
    {
        public AerendeRepository(AerendeContext aerendeContext)
        {
            _aerendeContext = aerendeContext;
        }

        private AerendeContext _aerendeContext { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cap"></param>
        /// <returns></returns>
        public async Task<List<PatientJournal>> GetAllPatientJournalWithCap(int cap)
        {
            var patientJournal = await _aerendeContext.GetJournalsWithChildren(cap);

            return patientJournal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PatientJournal> GetPatientJournalById(Guid id)
        {
            PatientJournal patientJournal = await _aerendeContext.GetPatientJournalById(id);

            return patientJournal;
        }


    }




}
