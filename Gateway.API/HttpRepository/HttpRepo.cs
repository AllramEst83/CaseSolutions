using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientService;
using HttpClientService.Helpers;

namespace Gateway.API.HttpRepository
{
    public class HttpRepo : IHttpRepo
    {
        public HttpService _httpService { get; }

        public HttpRepo(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<T> PostRequestWithContent<T>(HttpParameters httpParameters)
        {
            try
            {
                T result = await _httpService.PostStreamAsyncContent<T>(httpParameters);
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
                var result = await _httpService.GenericHttpGet<T>(httpParameters);
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
                T result = await _httpService.PostStreamAsyncQueryString<T>(httpParameters);
                return result;
            }
            catch (CustomApiException ex)
            {

                throw ex;
            }
        }

    }
}
