﻿using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContext;
using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.DataAccess.AerendeRepository
{
    public class AerendeRepository : IAerendeRepository
    {
        public AerendeRepository(AerendeContext aerendeContext)
        {
            _aerendeContext = aerendeContext;
        }

        private AerendeContext _aerendeContext { get; }


        public async Task<List<PatientJournal>> GetAllPatientJournalWithCap(int cap)
        {
            var patientJournal = await Task.FromResult(_aerendeContext.PatientJournals.Take(cap).ToList());

            return patientJournal;
        }

        public async Task<PatientJournal> GetPatientJournalById(Guid id)
        {
            PatientJournal patientJournal = await Task.FromResult(_aerendeContext.PatientJournals.SingleOrDefault(x => x.Id == id));

            return patientJournal;
        }


    }




}
