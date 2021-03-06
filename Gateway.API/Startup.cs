﻿using System.Text;
using CaseSolutionsTokenValidationParameters;
using FluentValidation.AspNetCore;
using Gateway.API.GatewayService;
using Gateway.API.Helpers;
using Gateway.API.HttpRepository;
using Gateway.API.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gateway.API
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
            //Bind JwtIssuerOptions to C# class
            IConfigurationSection JwtIssuerOptionsSection = Configuration.GetSection(Constants.JwtIssuer.JwtIssuerOptions);
            services.Configure<JwtIssuerOptions>(JwtIssuerOptionsSection);
            JwtIssuerOptions JwtIssuerOptionsSectionSettings = JwtIssuerOptionsSection.Get<JwtIssuerOptions>();
            //Bind JwtIssuerOptions to C# class 

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            //Get Symetrickey (!Should be Readonly Private!)
            SymmetricSecurityKey _signingKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(
                        ConfigHelper.AppSetting(
                            Constants.AppSettingStrings.AppSettings,
                            Constants.AppSettingStrings.Secret
                            )));

            //Moved JWT validation to a ClassLibrary. So it can be reused.
            services.AddValidationParameters(
                JwtIssuerOptionsSectionSettings.Issuer,
                JwtIssuerOptionsSectionSettings.Audience,
                _signingKey
                );

            //Services
            services.AddScoped<IHttpRepo, HttpRepo>();
            services.AddScoped<IGWService, GWService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //Develop this error handeling further in ErrorsController
            app.UseStatusCodePagesWithReExecute("/errors/{0}");



            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
