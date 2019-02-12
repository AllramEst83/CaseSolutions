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
        Task<T> Post<T>(HttpParameters httpParameters);
    }
}
