using Microsoft.EntityFrameworkCore;
using ResponseModels.DatabaseModels;

namespace Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContextFolder
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
        public DbSet<MedicalService> MedicalServices { get; set; }

        public DbSet<Adress> Adresses { get; set; }
        public DbSet<KindOfIllness> KindOfIllnesses { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        //Only migrate with this ppowerShell command (the other contexts will be auto generated)
        //dotnet ef database update --context AerendeContext
        //dotnet ef migrations add --> namn på migrationen: start
        //dotnet ef database update
    }
}
