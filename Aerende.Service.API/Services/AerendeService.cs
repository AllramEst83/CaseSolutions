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
        public IAerendeRepository AerendeRepository { get; }

        public AerendeService(IAerendeRepository aerendeRepository)
        {
            AerendeRepository = aerendeRepository;
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
            T result = await AerendeRepository.GetRequest<T>(httpParameters);

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
            T result = await AerendeRepository.PostRequestWithContent<T>(httpParameters);

            return result;
        }

    }
}
