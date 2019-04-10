using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResponseModels.Models;
using Auth.API.Services;
using Auth.API.Models;
using ResponseModels.ViewModels;
using Auth.API.Data.UserData.UserEntities.UserModel;

namespace Auth.API.Interfaces
{
    public interface IAccountsService
    {
        Task<bool> UserExists(string userEmailOrId);
        Task<bool> RoleExists(string role);
        Task<IdentityResult> CreateUser(User userIdentity, string password);
        Task<IdentityResult> AddRoleToUser(User userIdentity, string userRole);
        Task<IdentityResult> CreateRole(string role);
        Task<IdentityRole> GetRoleId(string role);
        Task<IdentityRole> GetRoleByName(string roleName);
        Task<UsersInRole> GetUsersInRole(string roleName);
        Task<IdentityResult> DeleteRole(IdentityRole identityRole);
        Task<User> GetUserById(string userId);
        Task<bool> UserHasRole(User user, string role);
        Task<IdentityResult> RemoveRolefromUser(User userIdentity, string userRole);
        Task<RolesForUser> GetUserRoles(User user);
        Task<AllRoles> GetAllRoles();
        Task<IdentityResult> RemoveRolesFromUser(User user, IList<string> userRoles);
        Task<IdentityResult> DeleteUser(User userIdentity);
        Task<IdentityResult> AddRolesToUser(User userIdentity, IEnumerable<string> userRoles);
        Task<AllUsers> GetListOfUsers();
        Task<User> GetUserByEmail(string email);

    }
}
