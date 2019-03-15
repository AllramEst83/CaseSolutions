using Database.Service.API.Data.TypeOfData.TypeOfEntities.TypeOfContext;
using Database.Service.API.DataAccess.Seeders.Interfaces;
using ResponseModels.DatabaseModels;
using System.Collections.Generic;

namespace Database.Service.API.DataAccess.Seeders
{
    public  class TypeOfSeeder: ITypeOfSeeder
    {
        public TypeOfSeeder(TypeOfContext typeOfContext)
        {
            _typeOfContext = typeOfContext;
        }

        public TypeOfContext _typeOfContext { get; }

        public void SeedTypeOf_s()
        {
            _typeOfContext.Database.EnsureCreated();

            _typeOfContext.TypeOfInsuranceWrappers.RemoveRange(_typeOfContext.TypeOfInsuranceWrappers);
            _typeOfContext.TypeOfDoctorWrappers.RemoveRange(_typeOfContext.TypeOfDoctorWrappers);
            _typeOfContext.TypeOfExaminationWrappers.RemoveRange(_typeOfContext.TypeOfExaminationWrappers);
            _typeOfContext.IllnessSeverityWrappers.RemoveRange(_typeOfContext.IllnessSeverityWrappers);

            _typeOfContext.SaveChanges();

            //IllnessSeverityWrapper
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

            _typeOfContext.IllnessSeverityWrappers.AddRange(illnessSeverityWrappers);
            _typeOfContext.SaveChanges();

            //TypeOfExaminationWrapper
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

            _typeOfContext.TypeOfExaminationWrappers.AddRange(typeOfExaminationWrappers);
            _typeOfContext.SaveChanges();

            //TypeOfInsuranceWrapper
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

            _typeOfContext.TypeOfInsuranceWrappers.AddRange(typeOfInsuranceWrappers);
            _typeOfContext.SaveChanges();

            //TypeOfDoctorWrappers
            List<TypeOfDoctorWrapper> typeOfDoctorWrappers = new List<TypeOfDoctorWrapper>()
            {
                 new TypeOfDoctorWrapper(){ TypeOfDoctor = TypeOfDoctor.GeneralMagicalAnimalDoctor },
                 new TypeOfDoctorWrapper(){ TypeOfDoctor = TypeOfDoctor.MagicHornDoctor },
                 new TypeOfDoctorWrapper(){ TypeOfDoctor = TypeOfDoctor.RainbowGlitterDoctor },
                 new TypeOfDoctorWrapper(){ TypeOfDoctor = TypeOfDoctor.RainbowTailDoctor },
                 new TypeOfDoctorWrapper(){ TypeOfDoctor = TypeOfDoctor.UnicornGeneralDoctor }              
               
            };

            _typeOfContext.TypeOfDoctorWrappers.AddRange(typeOfDoctorWrappers);
            _typeOfContext.SaveChanges();
        }
    }
}
