﻿using Auth.API.Models;
using Database.Service.API.Data.UserData.UserEntities.UserContext;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Auth.API.Helpers;
using Auth.API.AuthFactory;
using CaseSolutionsTokenValidationParameters;
using Auth.API.Interfaces;
using Auth.API.Services;

namespace Auth.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        //https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login

        public void ConfigureServices(IServiceCollection services)
        {

            //Bind AppSettingsJson to C# class
            IConfigurationSection appSettingsSection = Configuration.GetSection(Constants.AppSettingStrings.AppSettings);
            services.Configure<AppSettnigs>(appSettingsSection);
            AppSettnigs appSettings = appSettingsSection.Get<AppSettnigs>();
            //Bind AppSettingsJson to C# class

            //Bind JwtIssuerOptions to C# class
            IConfigurationSection JwtIssuerOptionsSection = Configuration.GetSection(Constants.JwtIssuer.JwtIssuerOptions);
            services.Configure<JwtIssuerOptions>(JwtIssuerOptionsSection);
            JwtIssuerOptions JwtIssuerOptionsSectionSettings = JwtIssuerOptionsSection.Get<JwtIssuerOptions>();
            //Bind JwtIssuerOptions to C# class     

            //Add Database
            services.AddDbContext<UserContext>(options =>
                 options.UseSqlServer(appSettings.UserConnection,
                    migrationOptions => migrationOptions.MigrationsAssembly(Constants.AppSettingStrings.AuthAPI)));

            //Add JWTFactory
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddScoped<IAccountsService, AccountsService>();

            //Get Symetrickey (!Should be Readonly Private!)
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));

            // Get options from app settings
            //var jwtAppSettningsOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            //string issuer = JwtIssuerOptionsSectionSettings.Issuer;
            //string audience = JwtIssuerOptionsSectionSettings.Audience;
            var signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = JwtIssuerOptionsSectionSettings.Issuer;
                options.Audience = JwtIssuerOptionsSectionSettings.Audience;
                options.SigningCredentials = signingCredentials;
            });

            //Moved JWT validation to a ClassLibrary. So it can be reused.
            services.AddValidationParameters(
                JwtIssuerOptionsSectionSettings.Issuer,
                JwtIssuerOptionsSectionSettings.Audience,
                _signingKey
                );

            #region
            ////AddTokenValidator
            //TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuer = true,
            //    ValidIssuer = JwtIssuerOptionsSectionSettings.Issuer,

            //    ValidateAudience = true,
            //    ValidAudience = JwtIssuerOptionsSectionSettings.Audience,

            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = _signingKey,

            //    RequireExpirationTime = false,
            //    ValidateLifetime = false,
            //    ClockSkew = TimeSpan.Zero
            //};

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(configureOptions =>
            //{
            //    configureOptions.ClaimsIssuer = JwtIssuerOptionsSectionSettings.Issuer;
            //    configureOptions.TokenValidationParameters = tokenValidationParameters;
            //    configureOptions.SaveToken = true;
            //});

            //// api user claim policy
            //services.AddAuthorization(options =>
            //{
            //    //Add more roles here to handel diffrent type of users: admin, user, editUser
            //    options.AddPolicy(TokenValidationConstants.Policies.AuthAPIAdmin, policy => policy.RequireClaim(TokenValidationConstants.Roles.Role, TokenValidationConstants.Roles.AdminAccess));
            //    options.AddPolicy(TokenValidationConstants.Policies.AuthAPICommonUser, policy => policy.RequireClaim(TokenValidationConstants.Roles.Role, TokenValidationConstants.Roles.CommonUserAccess));
            //});
            #endregion

            //AddIdentityModel
            var builder = services.AddIdentityCore<User>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();

            #region
            //services.AddIdentity<User, IdentityRole>(options =>
            //{
            //    options.User.RequireUniqueEmail = false;
            //})
            // .AddEntityFrameworkStores<UserContext>()
            // .AddDefaultTokenProviders();
            #endregion

            services.AddAutoMapper();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());


        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
