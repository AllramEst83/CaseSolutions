using Database.Service.API.Data.FakturaData.FakturaEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Data.FakturaData.FakturaEntities.FakturaContext
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {

        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<IllnessSev> IllnessSeveritys { get; set; }
        public DbSet<MedicalService> MedicalServices { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<TypeOfDoc> TypeOfDoctors { get; set; }
        public DbSet<TypeOfExamin> TypeOfExaminations { get; set; }
        public DbSet<KindOfIllness> KindOfIllnesses { get; set; }

        //dotnet ef migrations add --> namn på migrationen: start
        //dotnet ef database update
    }
}
