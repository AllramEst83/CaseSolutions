using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContext;
using Database.Service.API.Data.FakturaData.FakturaEntities.FakturaContext;
using Database.Service.API.Data.TypeOfData.TypeOfEntities.TypeOfContext;
using Database.Service.API.DataAccess.AerendeRepository;
using Database.Service.API.DataAccess.Seeders;
using Database.Service.API.DataAccess.Seeders.Interfaces;
using Database.Service.API.Helpers;
using Database.Service.API.Services;
using Database.Service.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IInvoiceSeeder, InvoiceSeeder>();
            services.AddScoped<IAerendeSeeder, AerendeSeeder>();
            services.AddScoped<ITypeOfSeeder, TypeOfSeeder>();

            services.AddScoped<IAerendeRepository, AerendeRepository>();

            services.AddScoped<IAerendeService, AerendeService>();

            services.AddDbContext<InvoiceContext>(config =>
            {
                config.UseSqlServer(ConfigHelper.AppSetting(DatabaseConstants.Connectionstrings, DatabaseConstants.InvoiceConnectionString));
            });

            services.AddDbContext<AerendeContext>(config =>
            {
                config.UseSqlServer(ConfigHelper.AppSetting(DatabaseConstants.Connectionstrings, DatabaseConstants.AerendeConnectionString));
            });

            services.AddDbContext<TypeOfContext>(config =>
            {
                config.UseSqlServer(ConfigHelper.AppSetting(DatabaseConstants.Connectionstrings, DatabaseConstants.TypeOfConnectionString));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            InvoiceContext invoiceContext,
            TypeOfContext typeOfContext,
            AerendeContext aerendeContext,
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
            app.UseMvc();
        }
    }
}
