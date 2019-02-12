using HttpClientService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API
{
    public interface IHttpRepo
    {
        Task<T> GetRequest<T>(HttpParameters httpParameters);
        Task<T> PostRequestWithContent<T>(HttpParameters httpParameters);
        Task<T> PostRequestWithQueryString<T>(HttpParameters httpParameters);
    }
}
