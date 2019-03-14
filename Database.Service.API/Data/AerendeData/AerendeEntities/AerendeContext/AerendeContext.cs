using Aerende.Service.API.Data;
using Database.Service.API.Data.AerendeData.AerendeEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContext
{
    public class AerendeContext : DbContext
    {
        public AerendeContext(DbContextOptions<AerendeContext> options) : base(options)
        {

        }

        public DbSet<PatientJournal> PatientJournals { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompanys { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public DbSet<Adress> Adresss { get; set; }
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<TypeOfInsuranceWrapper> TypeOfInsuranceWrappers { get; set; }
        public DbSet<TypeOfDoctorWrapper> TypeOfDoctorWrappers { get; set; }
        public DbSet<TypeOfExaminationWrapper> TypeOfExaminationWrappers { get; set; }
        public DbSet<IllnessSeverityWrapper> IllnessSeverityWrappers { get; set; }



        //dotnet ef migrations add --> namn på migrationen: start
        //dotnet ef database update
    }
}
