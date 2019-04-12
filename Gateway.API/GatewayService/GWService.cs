using Gateway.API.Interfaces;
using HttpClientService.Helpers;
using System.Threading.Tasks;

namespace Gateway.API.GatewayService
{

    public class GWService : IGWService
    {
        public IHttpRepo HttpRepo { get; }

        public GWService(IHttpRepo httpRepo)
        {
            HttpRepo = httpRepo;
        }

        //Get
        public async Task<T> Get<T>(HttpParameters httpParameters)
        {
            T result = await HttpRepo.GetRequest<T>(httpParameters);

            return result;
        }

        //Authenticate
        public async Task<T> Authenticate<T>(HttpParameters httpParameters)
        {
            T result = await HttpRepo.PostRequestWithContent<T>(httpParameters);

            return result;
        }

        //SignUp
        public async Task<T> SignUp<T>(HttpParameters httpParameters)
        {
            T result = await HttpRepo.PostRequestWithContent<T>(httpParameters);

            return result;
        }

        //AddRole
        public async Task<T> AddRole<T>(HttpParameters httpParameters)
        {
            T result = await HttpRepo.PostRequestWithContent<T>(httpParameters);

            return result;
        }

        //PostTo
        public async Task<T> PostTo<T>(HttpParameters httpParameters)
        {
            T result = await HttpRepo.PostRequestWithContent<T>(httpParameters);

            return result;
        }

        //GetHttpParameters
        //public HttpParameters GetHttpParameters(object model, string requestUrl, HttpMethod httpVerb, string id, string jwtToken = "")
        //{
        //    HttpParameters httpParameters =
        //      new HttpParameters
        //      {
        //          Content = model,
        //          HttpVerb = httpVerb,
        //          RequestUrl = requestUrl,
        //          Id = id,
        //          CancellationToken = CancellationToken.None,
        //          JwtToken = jwtToken
        //      };

        //    return httpParameters;
        //}


    }
}
