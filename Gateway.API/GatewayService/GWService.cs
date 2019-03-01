using Gateway.API.Helpers;
using Gateway.API.Interfaces;
using Gateway.API.ViewModels;
using HttpClientService.Helpers;
using System;
using System.Net.Http;
using System.Threading;
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

        //Get
        public async Task<T> Get<T>(HttpParameters httpParameters)
        {
            T result = await _httpRepo.GetRequest<T>(httpParameters);

            return result;
        }

        //Authenticate
        public async Task<T> Authenticate<T>(HttpParameters httpParameters)
        {
            T result = await _httpRepo.PostRequestWithContent<T>(httpParameters);

            return result;
        }

        //SignUp
        public async Task<T> SignUp<T>(HttpParameters httpParameters)
        {
            T result = await _httpRepo.PostRequestWithContent<T>(httpParameters);

            return result;
        }

        //AddRole
        public async Task<T> AddRole<T>(HttpParameters httpParameters)
        {
            T result = await _httpRepo.PostRequestWithContent<T>(httpParameters);

            return result;
        }

        //PostTo
        public async Task<T> PostTo<T>(HttpParameters httpParameters)
        {
            T result = await _httpRepo.PostRequestWithContent<T>(httpParameters);

            return result;
        }

        //GetHttpParameters
        public HttpParameters GetHttpParameters(object model, string requestUrl, HttpMethod httpVerb, string id, string jwtToken = "")
        {
            HttpParameters httpParameters =
              new HttpParameters
              {
                  Content = model,
                  HttpVerb = httpVerb,
                  RequestUrl = requestUrl,
                  Id = id,
                  CancellationToken = CancellationToken.None,
                  JwtToken = jwtToken
              };

            return httpParameters;
        }


    }
}
