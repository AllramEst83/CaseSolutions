﻿using Auth.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
using Microsoft.AspNetCore.Identity;
using Auth.API.ExceptionHandeling;

namespace Auth.API.Services
{
    public class AccountsService : CustomExceptionHandeling, IAccountsService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountsService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUser(User userIdentity, string password)
        {
            IdentityResult identityResult = null;

            identityResult = await TryCatch<ArgumentException, IdentityResult>(async () =>
             {
                 return await _userManager.CreateAsync(userIdentity, password);
             });

            return identityResult;
        }

        public async Task<IdentityResult> AddRoleToUser(User userIdentity, string userRole)
        {
            IdentityResult identityResult = null;

            identityResult = await TryCatch<ArgumentException, IdentityResult>(async () =>
            {
                return await _userManager.AddToRoleAsync(userIdentity, userRole);
            });

            return identityResult;
        }


        public async Task<IdentityResult> CreateRole(string role)
        {
            IdentityResult identityResult = null;

            identityResult = await TryCatch<ArgumentException, IdentityResult>(async () =>
            {
                IdentityRole indetityRole = new IdentityRole()
                {
                    Name = role
                };

                return await _roleManager.CreateAsync(indetityRole);

            });

            return identityResult;
        }

        public async Task<IdentityRole> GetRoleId(string role)
        {
            IdentityRole identityRole = new IdentityRole()
            {
                Name = role
            };

            string roleId = String.Empty;

            roleId = await TryCatch<ArgumentNullException, string>(async () =>
            {
                return await _roleManager.GetRoleIdAsync(identityRole);
            });

            identityRole.Id = roleId;

            return identityRole;
        }
        
        public async Task<IdentityResult> RemoveRolefromUser(User userIdentity, string userRole)
        {
            IdentityResult identityResult = null;

            identityResult = await TryCatch<ArgumentNullException, IdentityResult>(async () =>
            {
                return await _userManager.RemoveFromRoleAsync(userIdentity, userRole); ;
            });

            return identityResult;
        }

        public async Task<IdentityRole> GetRoleByName(string roleName)
        {
            IdentityRole identitytRole = null;

            identitytRole = await TryCatch<ArgumentNullException, IdentityRole>(async () =>
            {
                return await _roleManager.FindByNameAsync(roleName);
            });

            return identitytRole;
        }

        public async Task<User> GetUser(string userId)
        {
            User user = null;

            user = await TryCatch<ArgumentNullException, User>(async () =>
             {
                 return await _userManager.FindByIdAsync(userId);
             });

            return user;
        }

        public async Task<IList<User>> GetUsersInRole(string roleName)
        {
            IList<User> usersInRole = null;

            usersInRole = await TryCatch<ArgumentNullException, IList<User>>(async () =>
            {
                return await _userManager.GetUsersInRoleAsync(roleName);
            });

            return usersInRole;
        }

        public async Task<IdentityResult> DeleteRole(IdentityRole identityRole)
        {
            IdentityResult deleteRoleResult = null;

            deleteRoleResult = await TryCatch<ArgumentNullException, IdentityResult>(async () =>
            {
                return await _roleManager.DeleteAsync(identityRole);
            });

            return deleteRoleResult;
        }


        public async Task<bool> UserExists(string userEmailOrId)
        {
            Guid guidId = Guid.Empty;
            User userExist;

            if (Guid.TryParse(userEmailOrId, out guidId))
            {
                userExist = await TryCatch<ArgumentException, User>(async () =>
                   {
                       return await _userManager.FindByIdAsync(userEmailOrId);
                   });
            }
            else
            {
                userExist = await TryCatch<ArgumentException, User>(async () =>
                 {
                     return await _userManager.FindByEmailAsync(userEmailOrId);
                 });

            }

            return userExist == null ? false : true;
        }

        public async Task<bool> UserHasRole(User user, string role)
        {
            IList<string> userRoles = await TryCatch<ArgumentException, IList<string>>(async () =>
           {
               return await _userManager.GetRolesAsync(user);
           });

            bool userHasRole = userRoles.Any(x => x.Equals(role));

            return userHasRole;
        }

        public async Task<bool> RoleExists(string role)
        {
            bool roleExist = await TryCatch<ArgumentException, bool>(async () =>
            {
                return await _roleManager.RoleExistsAsync(role);
            });

            return roleExist;
        }


    }
}
