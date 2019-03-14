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
            context.Database.EnsureCreated();

            context.Invoices.RemoveRange(context.Invoices);
            context.MedicalServices.RemoveRange(context.MedicalServices);
            context.Doctores.RemoveRange(context.Doctores);
            context.KindOfIllnesses.RemoveRange(context.KindOfIllnesses);
            context.Prescriptions.RemoveRange(context.Prescriptions);
            context.IllnessSeveritys.RemoveRange(context.IllnessSeveritys);
            context.TypeOfDoctors.RemoveRange(context.TypeOfDoctors);
            context.TypeOfExaminations.RemoveRange(context.TypeOfExaminations);

            context.SaveChanges();

            List<TypeOfDoc> typeOfDoctors = new List<TypeOfDoc>()
            {
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.GeneralMagicalAnimalDoctor },
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.MagicHornDoctor },
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.RainbowGlitterDoctor },
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.RainbowTailDoctor },
                new TypeOfDoc(){ TypeOfDoctor = TypeOfDoctor.UnicornGeneralDoctor }
            };

            context.TypeOfDoctors.AddRange(typeOfDoctors);
            context.SaveChanges();

            List<TypeOfExamin> typeOfExaminations = new List<TypeOfExamin>()
            {
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.HeadExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.HoofExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.HornExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.LegExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.MagicalPropertiesExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.RainbowSparkleExamination},
                new TypeOfExamin(){TypeOfExamination = TypeOfExamination.TailExamination}
            };

            context.TypeOfExaminations.AddRange(typeOfExaminations);
            context.SaveChanges();

            List<IllnessSev> illnesSeverity = new List<IllnessSev>()
            {
                new IllnessSev(){IllnessSeverity = IllnessSeverity.Extreme},
                new IllnessSev(){IllnessSeverity = IllnessSeverity.High},
                new IllnessSev(){IllnessSeverity = IllnessSeverity.Low},
                new IllnessSev(){IllnessSeverity = IllnessSeverity.Minor},
                new IllnessSev(){IllnessSeverity = IllnessSeverity.Moderate},
                new IllnessSev(){IllnessSeverity = IllnessSeverity.None},
                new IllnessSev(){IllnessSeverity = IllnessSeverity.VeryHigh},
                new IllnessSev(){IllnessSeverity = IllnessSeverity.VeryLow},
                new IllnessSev(){IllnessSeverity = IllnessSeverity.VeryMinor}
            };

            context.IllnessSeveritys.AddRange(illnesSeverity);
            context.SaveChanges();

            List<Prescription> prescriptions = new List<Prescription>()
            {
                new Prescription(){Name = "Unicorn Sparkel Snarkel Remedy", Description = "This remedey is for unicorns only. Appply cream to non magic parts."},
                new Prescription(){Name = "Magic Tail In A Jiffy Spiffy", Description = "Wash the unicorn tail with this shampoo to imbunde the tail with rainbow colors."},
                new Prescription(){Name = "Fly like a Unicorn", Description = "Soothing creame for the unicorn wings. Kills feather eating magical bugs."}
            };

            context.Prescriptions.AddRange(prescriptions);
            context.SaveChanges();

            List<Doctor> listOfDoctors = new List<Doctor>()
            {
                new Doctor(){FirstName = "Allibaba", LastName ="Kurmedji", TypeOfDoctor = TypeOfDoctor.MagicHornDoctor},
                new Doctor(){FirstName = "Kay", LastName ="Efe Wiberg", TypeOfDoctor = TypeOfDoctor.UnicornGeneralDoctor},
                new Doctor(){FirstName ="Su", LastName ="Wiberg", TypeOfDoctor = TypeOfDoctor.RainbowGlitterDoctor}
            };

            context.Doctores.AddRange(listOfDoctors);
            context.SaveChanges();

            var illnessSeverity = context.IllnessSeveritys.ToList();

            List<KindOfIllness> kindOfIllnesses = new List<KindOfIllness>()
            {
                new KindOfIllness(){Title = "Magical Ass mushrooms",  IllnessSeverity = illnessSeverity[2]},
                new KindOfIllness(){Title = "Tail entangelment", IllnessSeverity = illnessSeverity[4]},
                new KindOfIllness(){Title = "Hoof change", IllnessSeverity = illnessSeverity[5] }
            };

            context.KindOfIllnesses.AddRange(kindOfIllnesses);
            context.SaveChanges();

            List<Doctor> listOfDoctorsForMedicalServices = context.Doctores.ToList();
            IEnumerable<Prescription> listOfPrescriptions = context.Prescriptions;

            DateTime startTime = new DateTime(2019, 03, 10, 15, 30, 00, DateTimeKind.Utc);
            DateTime endTime = new DateTime(2019, 03, 10, 16, 00, 00, DateTimeKind.Utc);

            DateTime startTimeTwo = startTime.AddHours(2);
            DateTime endTimeTwo = endTime.AddHours(3);

            DateTime startTimeThree = startTimeTwo.AddHours(2);
            DateTime endTimeThree = endTimeTwo.AddHours(3);

            List<KindOfIllness> kindOfIllnessesForMedicalServices = context.KindOfIllnesses.ToList();

            List<MedicalService> medicalServices = new List<MedicalService>()
            {
                new MedicalService()
                {
                    Id = Guid.NewGuid(),
                    Doctor = listOfDoctorsForMedicalServices[0],
                    Prescriptions = listOfPrescriptions.Take(1).ToList(),
                    StartTime = startTime,
                    EndTime = endTime,
                    ExaminationDuration = endTime - startTime,
                     HourlyCost = 650,
                     TypeOfExamination = TypeOfExamination.MagicalPropertiesExamination,
                     KindOfIllnes = kindOfIllnessesForMedicalServices[0],
                },
                new MedicalService()
                {
                    Id = Guid.NewGuid(),
                    Doctor = listOfDoctorsForMedicalServices[1],
                    StartTime = startTimeTwo,
                    EndTime = endTimeTwo,
                    ExaminationDuration = endTimeTwo - startTimeTwo,
                    HourlyCost = 250,
                    TypeOfExamination = TypeOfExamination.TailExamination,
                    Prescriptions = listOfPrescriptions.Take(2).ToList(),
                    KindOfIllnes = kindOfIllnessesForMedicalServices[1]
                },
                new MedicalService()
                {
                    Id = Guid.NewGuid(),
                    Doctor = listOfDoctorsForMedicalServices[2],
                    Prescriptions = listOfPrescriptions.Take(2).ToList(),
                    StartTime = startTimeThree,
                    EndTime = endTimeThree,
                    ExaminationDuration = endTimeThree - endTimeThree,
                    HourlyCost = 100,
                    TypeOfExamination = TypeOfExamination.RainbowSparkleExamination,
                    KindOfIllnes = kindOfIllnessesForMedicalServices[2]
                }
            };

            context.AddRange(medicalServices);
            context.SaveChanges();

            double discount = 0.20;
            List<MedicalService> medicalServicesForInvoices = context.MedicalServices.ToList();

            double one = double.Parse(medicalServicesForInvoices[0].ExaminationDuration.Minutes.ToString()) * medicalServicesForInvoices[0].HourlyCost;
            double two = double.Parse(medicalServicesForInvoices[1].ExaminationDuration.Minutes.ToString()) * medicalServicesForInvoices[0].HourlyCost;
            double three = double.Parse(medicalServicesForInvoices[2].ExaminationDuration.Minutes.ToString()) * medicalServicesForInvoices[0].HourlyCost;
            double totalSum = (one + two + three) * discount;

            var now = DateTime.Now;

            List<Invoice> invoices = new List<Invoice>()
            {
                new Invoice(){
                    MedicalServices = medicalServicesForInvoices,
                    IssueDate = now,
                    DueDate = now.AddDays(25),
                    Discount = discount,
                    TotalSum = totalSum
                }
            };

            context.Invoices.AddRange(invoices);
            context.SaveChanges();
        }

    }
}
