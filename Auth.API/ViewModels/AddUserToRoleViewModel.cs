using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.ViewModels
{
    public class AddUserToRoleViewModel
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Id { get; set; }
    }
}
