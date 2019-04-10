using Auth.API.Data.UserData.UserEntities.UserModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Data.UserData.UserEntities.UserContext
{
    public class AccountService : IdentityDbContext<User>
    {
        public AccountService(DbContextOptions<AccountService> options) : base(options) { }

        public DbSet<User> MyProperty { get; set; }

        //dotnet ef migrations add --> namn på migrationen: start
        //dotnet ef database update

    }
}
