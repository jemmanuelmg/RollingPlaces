<<<<<<< HEAD
﻿using RollingPlaces.Common.Models;
using System.Threading.Tasks;
=======
﻿using System.Threading.Tasks;
using RollingPlaces.Common.Models;
>>>>>>> RamaEmmanuel

namespace RollingPlaces.Common.Services
{
    public interface IApiService
    {
<<<<<<< HEAD
        Task<Response> GetPlaceAsync(string name, string urlBase, string servicePrefix, string controller);
    }
=======
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

        Task<bool> CheckConnectionAsync(string url);

        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);

        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken);

    }

>>>>>>> RamaEmmanuel
}
