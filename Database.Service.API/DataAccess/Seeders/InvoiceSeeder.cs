using Database.Service.API.Data.FakturaData.FakturaEntities.FakturaContext;
using Database.Service.API.Data.FakturaData.FakturaEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.DataAccess.Seeders
{
    public static class InvoiceSeeder
    {

        public static void SeedInvoices(this InvoiceContext context)
        {
           context.Database.EnsureCreatedAsync();

           GenerateTypeOfDoctors(context);

            GenerateTypeOfDoctors(context);
        }

        public static void EmptyDataBase(InvoiceContext context)
        {
            context.Invoices.RemoveRange(context.Invoices);
            context.Doctores.RemoveRange(context.Doctores);
            context.Illnesses.RemoveRange(context.Illnesses);
            context.IllnessSeveritys.RemoveRange(context.IllnessSeveritys);
            context.MedicalServices.RemoveRange(context.MedicalServices);
            context.Prescriptions.RemoveRange(context.Prescriptions);
            context.TypeOfDoctors.RemoveRange(context.TypeOfDoctors);
            context.TypeOfExaminations.RemoveRange(context.TypeOfExaminations);
        }

        public static void GenerateTypeOfDoctors(InvoiceContext context)
        {
            List<TypeOfDoc> typeOfDoctors = new List<TypeOfDoc>()
            {
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.GeneralMagicalAnimalDoctor },
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.MagicHornDoctor },
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.RainbowGlitterDoctor },
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.RainbowTailDoctor },
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.UnicornGeneralDoctor }
            };

            context.TypeOfDoctors.AddRange(typeOfDoctors);
            context.SaveChangesAsync();
        }

        public static void GenerateTypeOfExamination(InvoiceContext context)
        {
            List<TypeOfExamin> typeOfExaminations = new List<TypeOfExamin>()
            {
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.HeadExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.HoofExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.HornExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.LegExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.MagicalPropertiesExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.RainbowSparkleExamination},
                new TypeOfExamin(){ TypeOfExamination = TypeOfExamination.TailExamination}
            };

            context.TypeOfExaminations.AddRangeAsync(typeOfExaminations);
            context.SaveChangesAsync();
        }

    }
}
