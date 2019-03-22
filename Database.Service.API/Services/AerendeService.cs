﻿using APIErrorHandling;
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
            _aerendeRepository = aerendeRepository;
        }

        private IAerendeRepository _aerendeRepository { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cap"></param>
        /// <returns></returns>
        public async Task<AllPatientJournals> GetAllPatientJournalsWithCap(int cap)
        {
            var patientJournals = await TryCatch<ArgumentNullException, AllPatientJournals>(async () =>
            {
                List<PatientJournal> patientJournalsFromDatabase = await _aerendeRepository.GetAllPatientJournalWithCap(cap);

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
            var patientJournal = await TryCatch<ArgumentNullException, PatientJournal>(async () =>
            {
                PatientJournal patientResult = await _aerendeRepository.GetPatientJournalById(guid);

                return patientResult;
            });

            return patientJournal;
        }

    }
}
