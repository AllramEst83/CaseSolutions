using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContext;
using Database.Service.API.Data.FakturaData.FakturaEntities.FakturaContext;
using Database.Service.API.DataAccess.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;//<-Gör så att man kan använda "Include"
using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Database.Service.API.DataAccess.Seeders
{
    public class InvoiceSeeder : IInvoiceSeeder
    {
        public AerendeContext _aerendeContext { get; }
        public InvoiceContext _invoiceContext { get; }

        public InvoiceSeeder(AerendeContext aerendeContext, InvoiceContext invoiceContext)
        {
            _aerendeContext = aerendeContext;
            _invoiceContext = invoiceContext;
        }

        public void SeedInvoices()
        {
            _invoiceContext.Database.EnsureCreated();

            _invoiceContext.Invoices.RemoveRange(_invoiceContext.Invoices);
            _invoiceContext.SaveChanges();

            double discount = 0.20;
            double one = 30 * 2666;
            double two = 45 * 1500;
            double three = 35 * 2000;
            double totalSum = (one + two + three) * discount;
            var now = DateTime.Now;
            var patientJournal = _aerendeContext.PatientJournals.Include(x => x.MedicalServices).Include(x => x.Owners).LastOrDefault();
            List<Invoice> invoices = new List<Invoice>()
            {
                new Invoice()
                {
                    IssueDate = now,
                    DueDate = now.AddDays(25),
                    Discount = discount,
                    TotalSum = totalSum,
                    PatientJournalRelationalId = Guid.Parse("F7E8BAB5-1418-4B7F-A794-08D6A9724286")
                }
            };
            _invoiceContext.Invoices.AddRange(invoices);
            _invoiceContext.SaveChanges();


            //Old code for Seeding
            #region
            //List<Invoice> invoices = new List<Invoice>()
            //{
            //    new Invoice(){
            //        IssueDate = now,
            //        DueDate = now.AddDays(25),
            //        Discount = discount,
            //        TotalSum = totalSum,
            //        PatientJournal = new PatientJournal()
            //        {
            //            FirstName = patientJournal.FirstName,
            //            LastName = patientJournal.LastName,
            //            AnimalSSN = patientJournal.AnimalSSN,
            //            Clinic = new Clinic()
            //            {
            //                Adress = new Adress(){
            //                    StreetAdress = patientJournal.Clinic.Adress.StreetAdress,
            //                    Telephone = patientJournal.Clinic.Adress.Telephone,
            //                    ZipCode = patientJournal.Clinic.Adress.ZipCode
            //                },
            //                Id = patientJournal.Clinic.Id,
            //                Name = patientJournal.Clinic.Name,
            //                Doctors = new List<Doctor>()
            //                {

            //                    new Doctor()
            //                    {
            //                        TypeOfDoctorWrapper = new TypeOfDoctorWrapper(){TypeOfDoctor = patientJournal.Clinic.Doctors[0].TypeOfDoctorWrapper.TypeOfDoctor},
            //                        FirstName = patientJournal.Clinic.Doctors[0].FirstName,
            //                        LastName = patientJournal.Clinic.Doctors[0].LastName,
            //                        Id = patientJournal.Clinic.Doctors[0].Id

            //                    }
            //                }
            //            },
            //            Insurance = new Insurance()
            //            {
            //                Id = patientJournal.Insurance.Id,
            //                InsuranceCompany = new InsuranceCompany()
            //                {
            //                    Adress = new Adress()
            //                    {
            //                        StreetAdress = patientJournal.Insurance.InsuranceCompany.Adress.StreetAdress,
            //                        ZipCode = patientJournal.Insurance.InsuranceCompany.Adress.ZipCode,
            //                        Telephone = patientJournal.Insurance.InsuranceCompany.Adress.Telephone
            //                    },
            //                    Name = patientJournal.Insurance.InsuranceCompany.Name
            //                },
            //                TypeOfInsuranceWrapper = new TypeOfInsuranceWrapper(){ TypeOfInsurance = patientJournal.Insurance.TypeOfInsuranceWrapper.TypeOfInsurance}
            //            },
            //            MedicalServices = new List<MedicalService>()
            //            {
            //                new MedicalService()
            //                {
            //                    StartTime = patientJournal.MedicalServices[0].StartTime,
            //                    EndTime = patientJournal.MedicalServices[0].EndTime,
            //                    ExaminationDuration = patientJournal.MedicalServices[0].ExaminationDuration,
            //                    HourlyCost = patientJournal.MedicalServices[0].HourlyCost,
            //                    Id = patientJournal.MedicalServices[0].Id,
            //                    KindOfIllnes = new KindOfIllness()
            //                    {
            //                        Title = patientJournal.MedicalServices[0].KindOfIllnes.Title,
            //                        IllnessSeverity = new IllnessSeverityWrapper()
            //                        {
            //                            IllnessSeverity = IllnessSeverity.Minor
            //                        },
            //                        Id = patientJournal.MedicalServices[0].Id
            //                    },
            //                     Doctor = new Doctor()
            //                     {
            //                         Id = patientJournal.MedicalServices[0].Doctor.Id,
            //                         FirstName = patientJournal.MedicalServices[0].Doctor.FirstName,
            //                         LastName = patientJournal.MedicalServices[0].Doctor.LastName,
            //                          TypeOfDoctorWrapper = new TypeOfDoctorWrapper()
            //                          {
            //                              TypeOfDoctor = TypeOfDoctor.GeneralMagicalAnimalDoctor
            //                          }
            //                     },
            //                     Prescriptions = patientJournal.MedicalServices[0].Prescriptions,
            //                     TypeOfExaminationWrapper = new TypeOfExaminationWrapper(){
            //                         TypeOfExamination = TypeOfExamination.HeadExamination
            //                     }

            //                }
            //            },
            //            Owners = new List<Owner>()
            //            {
            //                new Owner(){
            //                    FirstName = patientJournal.Owners[0].FirstName,
            //                    LastName = patientJournal.Owners[0].LastName,
            //                    SSN = patientJournal.Owners[0].SSN,
            //                    Adress = new Adress()
            //                    {
            //                        StreetAdress = patientJournal.Owners[0].Adress.StreetAdress,
            //                        Telephone = patientJournal.Owners[0].Adress.Telephone,
            //                        ZipCode = patientJournal.Owners[0].Adress.ZipCode
            //                    }
            //                }
            //            },
            //            Id =  patientJournal.Id
            //        }
            //    },
            //};
            #endregion
        }

    }
}
