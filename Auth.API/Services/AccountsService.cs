using Auth.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Service.API.Data.UserData.UserEntities.UserModel;
using Microsoft.AspNetCore.Identity;
using ResponseModels.Models;
using Auth.API.Models;
using ResponseModels.ViewModels;
using APIErrorHandling;

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

        public async Task<IdentityResult> AddRolesToUser(User userIdentity, IEnumerable<string> userRoles)
        {
            IdentityResult identityResult = null;

            identityResult = await TryCatch<ArgumentException, IdentityResult>(async () =>
            {
                return await _userManager.AddToRolesAsync(userIdentity, userRoles);
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

        public async Task<AllUsers> GetListOfUsers()
        {
            AllUsers users = null;

            users = await TryCatch<ArgumentNullException, AllUsers>(async () =>
            {
                AllUsers allUsers = new AllUsers()
                {
                    _AllUsers = await Task.FromResult(_userManager.Users.Select(x =>
                            new UsersResponse
                            {
                                UserName = x.UserName,
                                Id = x.Id
                            }).ToList())
                };

                return allUsers;
            });

            return users ?? new AllUsers();
        }

        public async Task<UsersInRole> GetUsersInRole(string roleName)
        {
            UsersInRole usersInRole = null;
            usersInRole = await TryCatch<ArgumentNullException, UsersInRole>(async () =>
            {
                UsersInRole users = new UsersInRole()
                {
                    User = await _userManager.GetUsersInRoleAsync(roleName)
                };

                return users;
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

        public async Task<IdentityResult> DeleteUser(User userIdentity)
        {
            IdentityResult removeUserResult = null;

            removeUserResult = await TryCatch<ArgumentNullException, IdentityResult>(async () =>
            {
                return await _userManager.DeleteAsync(userIdentity);
            });

            return removeUserResult;
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

        public async Task<AllRoles> GetAllRoles()
        {
            AllRoles roles = null;

            roles = await TryCatch<ArgumentNullException, AllRoles>(async () =>
            {
                AllRoles allRoles = new AllRoles()
                {
                    _AllRoles = await Task.FromResult(_roleManager.Roles.Select(x =>
                                new GetAllRoles()
                                {
                                    Id = x.Id,
                                    RoleName = x.Name

                                }).ToList())
                };

                return allRoles;
            });

            return roles ?? new AllRoles();
        }

        public async Task<RolesForUser> GetUserRoles(User user)
        {
            RolesForUser userRoles = null;

            userRoles = await TryCatch<ArgumentNullException, RolesForUser>(async () =>
            {
                RolesForUser roles = new RolesForUser()
                {
                    Roles = await _userManager.GetRolesAsync(user),

                };

                return roles;
            });

            return userRoles;
        }

        public async Task<IdentityResult> RemoveRolesFromUser(User user, IList<string> userRoles)
        {
            IdentityResult removeRolesFromUserResult = null;

            removeRolesFromUserResult = await TryCatch<ArgumentNullException, IdentityResult>(async () =>
            {
                return await _userManager.RemoveFromRolesAsync(user, userRoles);
            });

            return removeRolesFromUserResult;
        }

        public async Task<bool> UserHasRole(User user, string role)
        {
            RolesForUser userRoles = null;

            userRoles = await GetUserRoles(user);

            bool userHasRole = userRoles.Roles.Any(x => x.Equals(role));

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
