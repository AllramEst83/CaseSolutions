﻿using Gateway.API.ViewModels;
using HttpClientService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.Interfaces
{
    public interface IGWService
    {
        Task<T> Get<T>(HttpParameters httpParameters);
        Task<T> Authenticate<T>(HttpParameters httpParameters);
        HttpParameters GetHttpParameters(LogInViewModel model);
        HttpParameters GetHttpParameters(RegistrationViewModel model);
        Task<T> SignUp<T>(HttpParameters httpParameters);
        HttpParameters GetHttpParameters(RoleToAddViewModel model);
        Task<T> AddRole<T>(HttpParameters httpParameters);
        Task<T> PostTo<T>(HttpParameters httpParameters);
        HttpParameters GetHttpParameters(AddUserToRoleViewModel model);
    }
}
