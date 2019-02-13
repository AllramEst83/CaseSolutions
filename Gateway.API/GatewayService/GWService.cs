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

        public HttpParameters GetHttpParameters(LogInViewModel model)
        {
            HttpParameters httpParameters =
              new HttpParameters
              {
                  Content = model,
                  HttpVerb = HttpMethod.Post,
                  RequestUrl = ConfigHelper.AppSetting(Constants.ServerUrls, Constants.Auth),
                  Id = Guid.Empty,
                  CancellationToken = CancellationToken.None
              };

            return httpParameters;
        }

    }
}
