
using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContextFolder;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ResponseModels.DatabaseModels;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Database.Service.API.Data.AerendeData.AerendeEntities.AerendeExtensions
{
    public static class AerendeQuerys
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cap"></param>
        /// <returns></returns>
        public static async Task<List<PatientJournal>> GetJournalsWithChildren(this AerendeContext context, int cap)
        {
            IIncludableQueryable<PatientJournal, Adress> patientJournals = context.GetChildrenInQuery();

            Task<List<PatientJournal>> patientJournlas_WithOrWithOutCap =
                                        cap == 0 ? patientJournals.ToListAsync() :
                                        patientJournals.Take(cap).ToListAsync();

            return await patientJournlas_WithOrWithOutCap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static IIncludableQueryable<PatientJournal, Adress> GetChildrenInQuery(this AerendeContext context)
        {
            return context.PatientJournals

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static async Task<PatientJournal> GetPatientJournalById(this AerendeContext context, Guid guid)
        {
            IIncludableQueryable<PatientJournal, Adress> patientJournal = context.GetChildrenInQuery();

            var singelPatientJournal = patientJournal.SingleOrDefaultAsync(x => x.Id == guid);

            return await singelPatientJournal;
        }
    }
}
