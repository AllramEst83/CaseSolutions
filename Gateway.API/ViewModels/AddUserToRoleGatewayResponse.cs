﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.ViewModels
{
    public class AddUserToRoleGatewayResponse
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}