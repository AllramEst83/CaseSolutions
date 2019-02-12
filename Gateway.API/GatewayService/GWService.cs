using Gateway.API.HttpRepository;
using Gateway.API.Interfaces;
using HttpClientService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.GatewayService
{

    public class GWService: IGWService
    {
        public IHttpRepo _httpRepo { get; }

        public GWService(IHttpRepo httpRepo)
        {
            _httpRepo = httpRepo;
        }

        public async Task<T> Get<T>(HttpParameters httpParameters)
        {
            T result = await _httpRepo.GetRequest<T>(httpParameters);

            return result;
        }

        public async Task<T> Authenticate<T>(HttpParameters httpParameters)
        {
            T result = await _httpRepo.PostRequestWithContent<T>(httpParameters);

            return result;
        }

    }
}
