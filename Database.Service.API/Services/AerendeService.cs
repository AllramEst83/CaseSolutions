using APIErrorHandling;
using Database.Service.API.DataAccess.AerendeRepository;
using Database.Service.API.Services.Interfaces;
using ResponseModels.DatabaseModels;
using ResponseModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Services
{
    public class AerendeService : CustomExceptionHandeling, IAerendeService
    {
        public AerendeService(IAerendeRepository aerendeRepository)
        {
            AerendeRepository = aerendeRepository;
        }

        private IAerendeRepository AerendeRepository { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cap"></param>
        /// <returns></returns>
        public async Task<AllPatientJournals> GetAllPatientJournalsWithCap(int cap)
        {
            AllPatientJournals patientJournals = await TryCatch<ArgumentNullException, AllPatientJournals>(async () =>
            {
                List<PatientJournal> patientJournalsFromDatabase = await AerendeRepository.GetAllPatientJournalWithCap(cap);

                return new AllPatientJournals() { PatentJournals = patientJournalsFromDatabase };
            });

            return patientJournals ?? new AllPatientJournals();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<PatientJournal> GetPatientJournalById(Guid guid)
        {
            PatientJournal patientJournal = await TryCatch<ArgumentNullException, PatientJournal>(async () =>
            {
                PatientJournal patientResult = await AerendeRepository.GetPatientJournalById(guid);

                return patientResult;
            });

            return patientJournal;
        }

    }
}
