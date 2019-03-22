using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseSolutionsTokenValidationParameters;
using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContextFolder;
using Database.Service.API.Data.FakturaData.FakturaEntities.FakturaContext;
using Database.Service.API.Data.TypeOfData.TypeOfEntities.TypeOfContext;
using Database.Service.API.DataAccess.AerendeRepository;
using Database.Service.API.DataAccess.Seeders;
using Database.Service.API.DataAccess.Seeders.Interfaces;
using Database.Service.API.Helpers;
using Database.Service.API.Services;
using Database.Service.API.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Database.Service.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                //AddFluentValidation
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            //Seeders
            services.AddScoped<IInvoiceSeeder, InvoiceSeeder>();
            services.AddScoped<IAerendeSeeder, AerendeSeeder>();
            services.AddScoped<ITypeOfSeeder, TypeOfSeeder>();

            //Repositories
            services.AddScoped<IAerendeRepository, AerendeRepository>();
            //AerendeServices
            services.AddScoped<IAerendeService, AerendeService>();

            //InvoiceContext
            services.AddDbContext<InvoiceContext>(config =>
            {
                config.UseSqlServer(ConfigHelper.AppSetting(DatabaseConstants.Connectionstrings, DatabaseConstants.InvoiceConnectionString));
            });

            //AerendeContext
            services.AddDbContext<AerendeContext>(config =>
            {
                config.UseSqlServer(ConfigHelper.AppSetting(DatabaseConstants.Connectionstrings, DatabaseConstants.AerendeConnectionString));
            });

            //TypeOfContext
            services.AddDbContext<TypeOfContext>(config =>
            {
                config.UseSqlServer(ConfigHelper.AppSetting(DatabaseConstants.Connectionstrings, DatabaseConstants.TypeOfConnectionString));
            });

            //Get Symetrickey (!Should be Readonly Private!)
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigHelper.AppSetting(DatabaseConstants.AppSettnings, DatabaseConstants.Secret)));

            var signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            var issuer = ConfigHelper.AppSetting(DatabaseConstants.JwtIssuerOptions, DatabaseConstants.Issuer);
            var audience = ConfigHelper.AppSetting(DatabaseConstants.JwtIssuerOptions, DatabaseConstants.Audience);

            //Moved JWT validation to a ClassLibrary. So it can be reused.
            services.AddValidationParameters(issuer, audience, _signingKey);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IInvoiceSeeder invoiceSeeder,
            ITypeOfSeeder typeOfSeeder,
            IAerendeSeeder aerendeSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            typeOfSeeder.SeedTypeOf_s();
            aerendeSeeder.SeedAerende();
            invoiceSeeder.SeedInvoices();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
