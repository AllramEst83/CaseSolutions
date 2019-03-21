using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.ViewModels
{
    public class JwtTokenContent
    {
        public Guid Id { get; set; }
        public string Auth_Token { get; set; }
        public string Expires_In { get; set; }
    }
}
