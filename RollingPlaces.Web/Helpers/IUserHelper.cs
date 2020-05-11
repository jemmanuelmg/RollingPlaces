using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RollingPlaces.Web.Data.Entities;
using RollingPlaces.Web.Models;

namespace RollingPlaces.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);

        Task<UserEntity> GetUserAsync(string email);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path);

        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);

        Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);

        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);

        Task<UserEntity> GetUserAsync(Guid userId);

        Task<string> GeneratePasswordResetTokenAsync(UserEntity user);

        Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string password);

        Task LogoutAsync();


    }
}

