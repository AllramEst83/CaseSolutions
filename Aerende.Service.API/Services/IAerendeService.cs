using HttpClientService.Helpers;
using System.Threading.Tasks;

namespace Aerende.Service.API.Services
{
    public interface IAerendeService
    {
        Task<T> Get<T>(HttpParameters httpParameters);
        Task<T> PostTo<T>(HttpParameters httpParameters);
    }
}