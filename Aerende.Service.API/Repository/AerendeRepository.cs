
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientService;
using HttpClientService.Helpers;

namespace Aerende.Service.API.Repository
{
    public class AerendeRepository : IAerendeRepository
    {
        public async Task<T> GetRequest<T>(HttpParameters httpParameters)
        {
            try
            {
                var result = await HttpService.GenericHttpGet<T>(httpParameters);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

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
    }
}
