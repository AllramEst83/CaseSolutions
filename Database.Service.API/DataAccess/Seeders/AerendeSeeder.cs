using Aerende.Service.API.Data;
using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContext;
using Database.Service.API.Data.AerendeData.AerendeEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.DataAccess.Seeders
{
    public static class AerendeSeeder
    {
        public static void SeedAerende(this AerendeContext context)
        {
            context.Database.EnsureCreated();

            context.PatientJournals.RemoveRange(context.PatientJournals);
            context.Insurances.RemoveRange(context.Insurances);
            context.InsuranceCompanys.RemoveRange(context.InsuranceCompanys);
            context.Clinics.RemoveRange(context.Clinics);
            context.Doctors.RemoveRange(context.Doctors);
            context.Owners.RemoveRange(context.Owners);

            context.Adresss.RemoveRange(context.Adresss);
            context.Illnesses.RemoveRange(context.Illnesses);
            context.Prescriptions.RemoveRange(context.Prescriptions);

            context.TypeOfInsuranceWrappers.RemoveRange(context.TypeOfInsuranceWrappers);
            context.TypeOfDoctorWrappers.RemoveRange(context.TypeOfDoctorWrappers);
            context.TypeOfExaminationWrappers.RemoveRange(context.TypeOfExaminationWrappers);
            context.IllnessSeverityWrappers.RemoveRange(context.IllnessSeverityWrappers);

            context.SaveChanges();

            List<IllnessSeverityWrapper> illnessSeverityWrappers = new List<IllnessSeverityWrapper>()
            {
                new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.Extreme},
                new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.High},
                new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.Low},
                new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.Minor},
                new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.Moderate},
                new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.None},
                new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.VeryHigh},
                new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.VeryLow},
                new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.VeryMinor}
            };

            context.IllnessSeverityWrappers.AddRange(illnessSeverityWrappers);
            context.SaveChanges();

            List<TypeOfExaminationWrapper> typeOfExaminationWrappers = new List<TypeOfExaminationWrapper>()
            {
                new TypeOfExaminationWrapper(){TypeOfExamination = TypeOfExamination.HeadExamination},
                new TypeOfExaminationWrapper(){TypeOfExamination = TypeOfExamination.HoofExamination},
                new TypeOfExaminationWrapper(){TypeOfExamination = TypeOfExamination.HornExamination},
                new TypeOfExaminationWrapper(){TypeOfExamination = TypeOfExamination.LegExamination},
                new TypeOfExaminationWrapper(){TypeOfExamination = TypeOfExamination.MagicalPropertiesExamination},
                new TypeOfExaminationWrapper(){TypeOfExamination = TypeOfExamination.RainbowSparkleExamination},
                new TypeOfExaminationWrapper(){TypeOfExamination = TypeOfExamination.TailExamination},
            };

            context.TypeOfExaminationWrappers.AddRange(typeOfExaminationWrappers);
            context.SaveChanges();

            List<TypeOfDoctorWrapper> typeOfDoctorWrappers = new List<TypeOfDoctorWrapper>()
            {
                new TypeOfDoctorWrapper(){TypeOfDoctor = TypeOfDoctor.GeneralMagicalAnimalDoctor},
                new TypeOfDoctorWrapper(){TypeOfDoctor = TypeOfDoctor.MagicHornDoctor},
                new TypeOfDoctorWrapper(){TypeOfDoctor = TypeOfDoctor.RainbowGlitterDoctor},
                new TypeOfDoctorWrapper(){TypeOfDoctor = TypeOfDoctor.RainbowTailDoctor},
                new TypeOfDoctorWrapper(){TypeOfDoctor = TypeOfDoctor.UnicornGeneralDoctor},
            };

            context.TypeOfDoctorWrappers.AddRange(typeOfDoctorWrappers);
            context.SaveChanges();

            List<TypeOfInsuranceWrapper> typeOfInsuranceWrappers = new List<TypeOfInsuranceWrapper>()
            {
                new TypeOfInsuranceWrapper(){TypeOfInsurance = TypeOfInsurance.HealthMaintenanceOrganization},
                new TypeOfInsuranceWrapper(){TypeOfInsurance = TypeOfInsurance.HealthSavingsAccount},
                new TypeOfInsuranceWrapper(){TypeOfInsurance = TypeOfInsurance.HighDeductibleHealthPlan},
                new TypeOfInsuranceWrapper(){TypeOfInsurance = TypeOfInsurance.PointOfServicePlan},
                new TypeOfInsuranceWrapper(){TypeOfInsurance = TypeOfInsurance.PreferredProviderOrganization},
                new TypeOfInsuranceWrapper(){TypeOfInsurance = TypeOfInsurance.SavingsStyleAddOn},
                new TypeOfInsuranceWrapper(){TypeOfInsurance = TypeOfInsurance.SpendingAccount},
            };

            context.TypeOfInsuranceWrappers.AddRange(typeOfInsuranceWrappers);
            context.SaveChanges();

            List<Prescription> prescriptions = new List<Prescription>()
            {
                // FROM HERE AND UPWARDS
            };
        }
    }
}
