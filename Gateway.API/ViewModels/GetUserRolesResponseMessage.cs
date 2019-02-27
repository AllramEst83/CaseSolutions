using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.ViewModels
{
    public class GetUserRolesResponseMessage
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string UserId { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
