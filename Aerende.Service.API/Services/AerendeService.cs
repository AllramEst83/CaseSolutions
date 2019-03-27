using Aerende.Service.API.Repository;
using HttpClientService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aerende.Service.API.Services
{
    public class AerendeService : IAerendeService
    {
        public IAerendeRepository _aerendeRepository { get; }

        public AerendeService(IAerendeRepository aerendeRepository)
        {
            _aerendeRepository = aerendeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpParameters"></param>
        /// <returns></returns>
        //Get
        public async Task<T> Get<T>(HttpParameters httpParameters)
        {
            T result = await _aerendeRepository.GetRequest<T>(httpParameters);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpParameters"></param>
        /// <returns></returns>
        //PostTo
        public async Task<T> PostTo<T>(HttpParameters httpParameters)
        {
            T result = await _aerendeRepository.PostRequestWithContent<T>(httpParameters);

            return result;
        }

    }
}
