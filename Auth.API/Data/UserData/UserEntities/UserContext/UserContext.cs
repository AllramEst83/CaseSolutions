using Database.Service.API.Data.UserData.UserEntities.UserModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Data.UserData.UserEntities.UserContext
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> MyProperty { get; set; }

        //dotnet ef migrations add --> namn på migrationen: start
        //dotnet ef database update

    }
}
