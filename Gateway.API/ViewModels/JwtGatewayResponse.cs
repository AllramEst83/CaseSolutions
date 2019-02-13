using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.ViewModels
{
    public class JwtGatewayResponse
    {
        public string Id { get; set; }
        public string Auth_Token { get; set; }
        public string Expires_In { get; set; }
        public int StatusCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
