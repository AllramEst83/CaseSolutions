using Auth.API.Data.UserData.UserEntities.UserModel;
using Auth.API.Interfaces;
using AutoMapper;
using CaseSolutionsTokenValidationParameters.Models;
using System.Collections.Generic;

namespace Auth.API.DataAccess.Seeder
{
    public class AuthSeeder : IAuthSeeder
    {
        public IAccountsService AccountService { get; }
        public IMapper Mappaer { get; }

        public AuthSeeder(IAccountsService accountService, IMapper mapper)
        {
            AccountService = accountService;
            Mappaer = mapper;
        }

        public async void SeedUser()
        {

            if (!await AccountService.RoleExists(TokenValidationConstants.Roles.AdminAccess))
            {
                await AccountService.CreateRole(TokenValidationConstants.Roles.AdminAccess);
                await AccountService.CreateRole(TokenValidationConstants.Roles.EditUserAccess);
                await AccountService.CreateRole(TokenValidationConstants.Roles.CommonUserAccess);
            }
                       
            string password = "test123!!";

            User userIdentity = new User()
            {
                Email = "Kajan@altavisat.com",
                FirstName = "Abu",
                LastName = "Svängsta"
            };

            if (!await AccountService.UserExists(userIdentity.Email))
            {
                await AccountService.CreateUser(userIdentity, password);
            }

            if (!await AccountService.UserHasRole(userIdentity, TokenValidationConstants.Roles.AdminAccess))
            {
                User userToAddRTolesTo = await AccountService.GetUserByEmail(userIdentity.UserName);

                List<string> listOfRolesToAdd = new List<string>()
                {
                    TokenValidationConstants.Roles.AdminAccess,
                    TokenValidationConstants.Roles.EditUserAccess,
                    TokenValidationConstants.Roles.CommonUserAccess
                };

                await AccountService.AddRolesToUser(userToAddRTolesTo, listOfRolesToAdd);
            }

        }
    }
}
