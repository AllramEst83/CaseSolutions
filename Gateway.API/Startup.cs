using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseSolutionsTokenValidationParameters;
using FluentValidation.AspNetCore;
using Gateway.API.GatewayService;
using Gateway.API.Helpers;
using Gateway.API.HttpRepository;
using Gateway.API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigHelper.AppSetting("AppSettings", "Secret")));


            services.AddValidationParameters(JwtIssuerOptionsSectionSettings.Issuer, JwtIssuerOptionsSectionSettings.Audience, _signingKey);

            //AddTokenValidator
            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuer = true,
            //    ValidIssuer = ConfigHelper.AppSetting("JwtIssuerOptions", "Issuer"),

            //    ValidateAudience = true,
            //    ValidAudience = ConfigHelper.AppSetting("JwtIssuerOptions", "Audience"),

            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = _signingKey,

            //    RequireExpirationTime = true,
            //    ValidateLifetime = true,
            //    ClockSkew = TimeSpan.Zero
            //};

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(configureOptions =>
            //{
            //    configureOptions.ClaimsIssuer = ConfigHelper.AppSetting("JwtIssuerOptions", "Issuer");
            //    configureOptions.TokenValidationParameters = tokenValidationParameters;
            //    configureOptions.SaveToken = true;
            //});

            //// api user claim policy
            //services.AddAuthorization(options =>
            //{
            //    //Add more roles here to handel diffrent type of users: admin, user, editUser
            //    options.AddPolicy(Constants.GatewayAPIAdmin, policy => policy.RequireClaim("role", "adminAccess"));
            //    options.AddPolicy(Constants.GatewayAPICommonUser, policy => policy.RequireClaim("role", "commonUserAccess"));
            //});

            //Services
            services.AddScoped<IHttpRepo, HttpRepo>();
            services.AddScoped<IGWService, GWService>();

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
