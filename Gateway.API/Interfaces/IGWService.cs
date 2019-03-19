using HttpClientService.Helpers;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.API.Interfaces
{
    public interface IGWService
    {
        Task<T> Get<T>(HttpParameters httpParameters);
        Task<T> Authenticate<T>(HttpParameters httpParameters);
        Task<T> SignUp<T>(HttpParameters httpParameters);
        Task<T> AddRole<T>(HttpParameters httpParameters);
        Task<T> PostTo<T>(HttpParameters httpParameters);
        //HttpParameters GetHttpParameters(object model, string requestUrl, HttpMethod httpVerb, string id, string jwtToken = "");
    }
}
