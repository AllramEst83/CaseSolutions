
using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContextFolder;
using Database.Service.API.Data.FakturaData.FakturaEntities.FakturaContext;
using Database.Service.API.Data.TypeOfData.TypeOfEntities.TypeOfContext;
using Database.Service.API.DataAccess.Seeders.Interfaces;
using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.DataAccess.Seeders
{
    public class AerendeSeeder : IAerendeSeeder
    {
        public AerendeSeeder(
            AerendeContext aerendeContext,
            TypeOfContext typeOfContext)
        {
            _aerendeContext = aerendeContext;
            _typeOfContext = typeOfContext;
        }

        public AerendeContext _aerendeContext { get; }
        public TypeOfContext _typeOfContext { get; }


        public void SeedAerende()
        {

            _aerendeContext.Database.EnsureCreated();

            _aerendeContext.PatientJournals.RemoveRange(_aerendeContext.PatientJournals);
            _aerendeContext.MedicalServices.RemoveRange(_aerendeContext.MedicalServices);
            _aerendeContext.Insurances.RemoveRange(_aerendeContext.Insurances);
            _aerendeContext.InsuranceCompanys.RemoveRange(_aerendeContext.InsuranceCompanys);
            _aerendeContext.Clinics.RemoveRange(_aerendeContext.Clinics);
            _aerendeContext.Doctors.RemoveRange(_aerendeContext.Doctors);
            _aerendeContext.Owners.RemoveRange(_aerendeContext.Owners);

            _aerendeContext.Adresses.RemoveRange(_aerendeContext.Adresses);
            _aerendeContext.KindOfIllnesses.RemoveRange(_aerendeContext.KindOfIllnesses);
            _aerendeContext.Prescriptions.RemoveRange(_aerendeContext.Prescriptions);

            _aerendeContext.SaveChanges();


            //Prescriptions
            List<Prescription> prescriptions = new List<Prescription>()
            {
                new Prescription(){Name = "Unicorn Sparkel Snarkel Remedy", Description = "This remedey is for unicorns only. Appply cream to non magic parts."},
                new Prescription(){Name = "Magic Tail In A Jiffy Spiffy", Description = "Wash the unicorn tail with this shampoo to imbunde the tail with rainbow colors."},
                new Prescription(){Name = "Fly like a Unicorn", Description = "Soothing creame for the unicorn wings. Kills feather eating magical bugs."}
            };

            _aerendeContext.Prescriptions.AddRange(prescriptions);
            _aerendeContext.SaveChanges();

            List<KindOfIllness> kindOfIllnesses = new List<KindOfIllness>()
            {
                new KindOfIllness(){Title = "Magical Ass mushrooms",  IllnessSeverity = new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.Minor } },
                new KindOfIllness(){Title = "Tail entangelment", IllnessSeverity = new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.Minor }},
                new KindOfIllness(){Title = "Hoof change", IllnessSeverity = new IllnessSeverityWrapper(){IllnessSeverity = IllnessSeverity.Minor }}
            };

            _aerendeContext.KindOfIllnesses.AddRange(kindOfIllnesses);
            _aerendeContext.SaveChanges();

            List<Adress> adresses = new List<Adress>()
            {
                new Adress(){StreetAdress = "Gustavianum 66", Telephone = "08123456789", ZipCode ="98718"},
                new Adress(){StreetAdress = "RulleGatan 7", Telephone = "074562365", ZipCode ="45632"},
                new Adress(){StreetAdress = "PastorsVägen 88", Telephone = "1234567825", ZipCode ="325698"},
            };

            _aerendeContext.Adresses.AddRange(adresses);
            _aerendeContext.SaveChanges();

            List<Adress> adressesForOwner = _aerendeContext.Adresses.ToList();
            List<Owner> owners = new List<Owner>()
            {
                new Owner(){FirstName ="Kurre", LastName = "Snigelfart", Adress = adressesForOwner[0], SSN = 198312120432},
                new Owner(){FirstName ="Abbas", LastName = "Gringo", Adress = adressesForOwner[1], SSN = 196505126666},
                new Owner(){FirstName ="Norbert", LastName = "Rataxes", Adress = adressesForOwner[2], SSN = 200010259987},
            };

            _aerendeContext.Owners.AddRange(owners);
            _aerendeContext.SaveChanges();


            var typOfDoctor = _typeOfContext.TypeOfDoctorWrappers.ToList();
            List<Doctor> listOfDoctors = new List<Doctor>()
            {
                new Doctor(){FirstName = "Allibaba", LastName ="Kurmedji", TypeOfDoctorWrapper =  new TypeOfDoctorWrapper(){TypeOfDoctor = TypeOfDoctor.GeneralMagicalAnimalDoctor } },
                new Doctor(){FirstName = "Kay", LastName ="Efe Wiberg", TypeOfDoctorWrapper =  new TypeOfDoctorWrapper(){TypeOfDoctor = TypeOfDoctor.RainbowGlitterDoctor }  },
                new Doctor(){FirstName ="Su", LastName ="Wiberg", TypeOfDoctorWrapper = new TypeOfDoctorWrapper(){TypeOfDoctor = TypeOfDoctor.UnicornGeneralDoctor }  }
            };

            _aerendeContext.Doctors.AddRange(listOfDoctors);
            _aerendeContext.SaveChanges();


            var doctorsForClinics = _aerendeContext.Doctors.ToList();
            List<Clinic> clinics = new List<Clinic>()
            {
                new Clinic(){Name ="Magical Horse resort", Adress = new Adress(){StreetAdress ="Blåklintsvägen 6", Telephone ="123456789", ZipCode ="12356" },Doctors = new  List<Doctor>(){ new Doctor {FirstName = doctorsForClinics[0].FirstName, LastName = doctorsForClinics[0].LastName, TypeOfDoctorWrapper = doctorsForClinics[0].TypeOfDoctorWrapper } } },
                new Clinic(){Name ="Unicorn Treatment Center", Adress = new Adress(){StreetAdress ="Hästgränd 43", Telephone ="123256589", ZipCode ="456984" },Doctors =  new  List<Doctor>(){ new Doctor {FirstName = doctorsForClinics[1].FirstName, LastName = doctorsForClinics[1].LastName, TypeOfDoctorWrapper = doctorsForClinics[1].TypeOfDoctorWrapper } }},
                new Clinic(){Name ="Magical Creatures and other stuff", Adress = new Adress(){StreetAdress ="Smittovägen 1", Telephone ="654873365", ZipCode ="85236" },Doctors =   new  List<Doctor>(){ new Doctor {FirstName = doctorsForClinics[2].FirstName, LastName = doctorsForClinics[2].LastName, TypeOfDoctorWrapper = doctorsForClinics[2].TypeOfDoctorWrapper } }},
            };

            _aerendeContext.Clinics.AddRange(clinics);
            _aerendeContext.SaveChanges();


            List<InsuranceCompany> insuranceCompanies = new List<InsuranceCompany>()
            {
                new InsuranceCompany(){Name ="Magical Animal Insurance", Adress = new Adress(){StreetAdress = "Skamgatan 77", Telephone ="0855498639", ZipCode ="12356" } },
                new InsuranceCompany(){Name ="Beast Insurances", Adress = new Adress(){StreetAdress = "Fredsgatan 45", Telephone ="0855498639", ZipCode ="12356"} },
                new InsuranceCompany(){Name ="Government Standard Insurance For Magical Horses", Adress = new Adress(){StreetAdress = "Fuskväg 1", Telephone ="0855498639", ZipCode ="12356"} },
            };

            _aerendeContext.InsuranceCompanys.AddRange(insuranceCompanies);
            _aerendeContext.SaveChanges();


            var insuranceCompaniesForinsurance = _aerendeContext.InsuranceCompanys.ToList();
            List<Insurance> insurances = new List<Insurance>()
            {
                new Insurance(){InsuranceCompany = insuranceCompaniesForinsurance[0], TypeOfInsuranceWrapper = new TypeOfInsuranceWrapper(){ TypeOfInsurance = TypeOfInsurance.HealthMaintenanceOrganization } },
                new Insurance(){InsuranceCompany = insuranceCompaniesForinsurance[1], TypeOfInsuranceWrapper = new TypeOfInsuranceWrapper(){ TypeOfInsurance = TypeOfInsurance.PointOfServicePlan } },
                new Insurance(){InsuranceCompany = insuranceCompaniesForinsurance[2], TypeOfInsuranceWrapper = new TypeOfInsuranceWrapper(){ TypeOfInsurance = TypeOfInsurance.SpendingAccount } },
            };

            _aerendeContext.Insurances.AddRange(insurances);
            _aerendeContext.SaveChanges();


            List<Doctor> listOfDoctorsForMedicalServices = _aerendeContext.Doctors.ToList();
            List<Prescription> listOfPrescriptions = _aerendeContext.Prescriptions.ToList();

            DateTime startTime = new DateTime(2019, 03, 10, 15, 30, 00, DateTimeKind.Utc);
            DateTime endTime = new DateTime(2019, 03, 10, 16, 00, 00, DateTimeKind.Utc);

            DateTime startTimeTwo = startTime.AddHours(2);
            DateTime endTimeTwo = endTime.AddHours(3);

            DateTime startTimeThree = startTimeTwo.AddHours(2);
            DateTime endTimeThree = endTimeTwo.AddHours(3);

            List<KindOfIllness> kindOfIllnessesForMedicalServices = _aerendeContext.KindOfIllnesses.ToList();
            List<TypeOfExaminationWrapper> typeOfExaminationsForMedicalServices = _typeOfContext.TypeOfExaminationWrappers.ToList();

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
                    TypeOfExaminationWrapper = new TypeOfExaminationWrapper(){ TypeOfExamination = typeOfExaminationsForMedicalServices[0].TypeOfExamination },
                    KindOfIllnes = kindOfIllnessesForMedicalServices[0]
                },
                new MedicalService()
                {
                    Id = Guid.NewGuid(),
                    Doctor = listOfDoctorsForMedicalServices[1],
                    StartTime = startTimeTwo,
                    EndTime = endTimeTwo,
                    ExaminationDuration = endTimeTwo - startTimeTwo,
                    HourlyCost = 250,
                    TypeOfExaminationWrapper = new TypeOfExaminationWrapper(){ TypeOfExamination = typeOfExaminationsForMedicalServices[3].TypeOfExamination },
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
                    TypeOfExaminationWrapper = new TypeOfExaminationWrapper(){ TypeOfExamination = typeOfExaminationsForMedicalServices[5].TypeOfExamination },
                    KindOfIllnes = kindOfIllnessesForMedicalServices[2]
                }
            };

            _aerendeContext.MedicalServices.AddRange(medicalServices);
            _aerendeContext.SaveChanges();


            List<Clinic> clinicsForPatientJournal = _aerendeContext.Clinics.ToList();
            List<Insurance> insurancesForPatientJournal = _aerendeContext.Insurances.ToList();
            List<MedicalService> medicalServicesForPatientJournal = _aerendeContext.MedicalServices.ToList();
            List<Owner> ownersForPOatientJournal = _aerendeContext.Owners.ToList();
            List<PatientJournal> patientJournals = new List<PatientJournal>()
            {
                new PatientJournal(){
                        FirstName ="Snabbe",
                LastName = "Flabben",
                AnimalSSN = "15506305598",
                Clinic = clinicsForPatientJournal[2],
                Insurance = insurancesForPatientJournal[2],
                MedicalServices = medicalServicesForPatientJournal.Take(3).ToList(),
                Owners = ownersForPOatientJournal.Take(2).ToList()
                },
                  new PatientJournal(){
                      FirstName ="Hulli",
                LastName = "Gulli",
                AnimalSSN = "15506305598",
                Clinic = clinicsForPatientJournal[2],
                Insurance = insurancesForPatientJournal[2],
                MedicalServices = medicalServicesForPatientJournal.Take(3).ToList(),
                Owners = ownersForPOatientJournal.Take(2).ToList()
                },
                    new PatientJournal(){
                    FirstName ="Gun",
                    LastName ="Powder",
                    AnimalSSN = "15506305598",
                    Clinic = clinicsForPatientJournal[2],
                    Insurance = insurancesForPatientJournal[2],
                    MedicalServices = medicalServicesForPatientJournal.Take(3).ToList(),
                    Owners = ownersForPOatientJournal.Take(2).ToList()
                },
            };

            _aerendeContext.PatientJournals.AddRange(patientJournals);
            _aerendeContext.SaveChanges();

        }
    }
}
