using HttpClientService;
using HttpClientService.Helpers;
using System.Threading.Tasks;

namespace Gateway.API.HttpRepository
{
    public class HttpRepo : IHttpRepo
    {

        public async Task<T> PostRequestWithContent<T>(HttpParameters httpParameters)
        {
            try
            {
                T result = await HttpService.PostStreamAsyncContent<T>(httpParameters);
                return result;
            }
            catch (CustomApiException ex)
            {

                throw ex;
            }
        }
        
        public async Task<T> GetRequest<T>(HttpParameters httpParameters)
        {
            try
            {
                var result = await HttpService.GenericHttpGet<T>(httpParameters);
                return result;
            }
            catch (CustomApiException ex)
            {

                throw ex;
            }
        }

        public async Task<T> PostRequestWithQueryString<T>(HttpParameters httpParameters)
        {
            try
            {
                T result = await HttpService.PostStreamAsyncQueryString<T>(httpParameters);
                return result;
            }
            catch (CustomApiException ex)
            {

                throw ex;
            }
        }

    }
}
