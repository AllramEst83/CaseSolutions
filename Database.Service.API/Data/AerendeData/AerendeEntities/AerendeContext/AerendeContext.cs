using Microsoft.EntityFrameworkCore;
using ResponseModels.DatabaseModels;

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
        public DbSet<MedicalService> MedicalServices { get; set; }

        public DbSet<Adress> Adresses { get; set; }
        public DbSet<KindOfIllness> KindOfIllnesses { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }



        //dotnet ef migrations add --> namn på migrationen: start
        //dotnet ef database update
    }
}
