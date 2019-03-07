using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Service.API.Data.UserData.UserEntities.UserModel;

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
        Task<IList<User>> GetUsersInRole(string roleName);
        Task<IdentityResult> DeleteRole(IdentityRole identityRole);
        Task<User> GetUser(string userId);
        Task<bool> UserHasRole(User user, string role);
        Task<IdentityResult> RemoveRolefromUser(User userIdentity, string userRole);
    }
}
