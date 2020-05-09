using RollingPlaces.Common.Models;
using System.Threading.Tasks;

namespace RollingPlaces.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetPlaceAsync(string name, string urlBase, string servicePrefix, string controller);
    }
}
