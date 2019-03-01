﻿using APIResponseMessageWrapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.ViewModels
{
    public class GetAllRolesResponseMessage
    {
        public List<GetAllRoles> ListOfAllRoles { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
