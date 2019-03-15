using Microsoft.EntityFrameworkCore;
using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Data.TypeOfData.TypeOfEntities.TypeOfContext
{
    public class TypeOfContext : DbContext
    {
        public TypeOfContext(DbContextOptions<TypeOfContext> options) : base(options)
        {

        }
        
        public DbSet<TypeOfInsuranceWrapper> TypeOfInsuranceWrappers { get; set; }
        public DbSet<TypeOfDoctorWrapper> TypeOfDoctorWrappers { get; set; }
        public DbSet<TypeOfExaminationWrapper> TypeOfExaminationWrappers { get; set; }
        public DbSet<IllnessSeverityWrapper> IllnessSeverityWrappers { get; set; }


        //dotnet ef migrations add --> namn på migrationen: start
        //dotnet ef database update
    }
}
