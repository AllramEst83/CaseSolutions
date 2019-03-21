
using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContextFolder;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ResponseModels.DatabaseModels;

namespace Database.Service.API.Data.AerendeData.AerendeEntities.AerendeExtensions
{
    public static class AerendeQuerys
    {
        public static List<PatientJournal> GetJournalsWithChildren(this AerendeContext context, int cap)
        {

            var patientJournals = context.PatientJournals

               .OrderBy(x => x.FirstName)

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
                .ThenInclude(x => x.Adress);

            var patientJournlas_WithOrWithOutCap = 
                cap == 0 ? patientJournals.ToList() : 
                patientJournals.Take(cap).ToList();

            return patientJournlas_WithOrWithOutCap;
        }
    }
}
