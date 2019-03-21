using HttpClientService.Helpers;
using System.Threading.Tasks;

namespace Aerende.Service.API.Repository
{
    public interface IAerendeRepository
    {
        Task<T> GetRequest<T>(HttpParameters httpParameters);
        Task<T> PostRequestWithContent<T>(HttpParameters httpParameters);
    }
}