using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContext;
using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;//<-Gör så att man kan använda "Include"

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
            var patientJournal = await Task.FromResult(
                _aerendeContext.PatientJournals
                .Include(x => x.Clinic)
                .ThenInclude(x => x.Adress)

                .Include(x => x.Clinic)
                .ThenInclude(x => x.Doctors)
                .ThenInclude(x => x.TypeOfDoctorWrapper)

                .Include(x => x.MedicalServices)
                .ThenInclude(x => x.Doctor)

                .Include(x => x.MedicalServices)
                .ThenInclude(x => x.TypeOfExaminationWrapper)

                .Include(x => x.MedicalServices)
                .ThenInclude(x => x.KindOfIllnes)
                .ThenInclude(x => x.IllnessSeverity)

                .Include(x => x.MedicalServices)
                .ThenInclude(x => x.Prescriptions)

                 .Include(x => x.Insurance)
                 .ThenInclude(x => x.TypeOfInsuranceWrapper)

                 .Include(x => x.Insurance)
                 .ThenInclude(x => x.InsuranceCompany)
                 .ThenInclude(x => x.Adress)


                .Include(x => x.Owners)
                .ThenInclude(x => x.Adress).ToList());

            return patientJournal;
        }

        public async Task<PatientJournal> GetPatientJournalById(Guid id)
        {
            PatientJournal patientJournal = await Task.FromResult(_aerendeContext.PatientJournals.SingleOrDefault(x => x.Id == id));

            return patientJournal;
        }


    }




}
