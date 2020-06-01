using RollingPlaces.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RollingPlaces.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PicturePath { get; set; }

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName}";

        public LoginType LoginType { get; set; }

        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
        ? "https://rollingplacesweb.azurewebsites.net//images/noimage.png"
        //public string PictureFullPath => string.IsNullOrEmpty(PicturePath) ? "https://rollingplacesweb.azurewebsites.net//images/noimage.png" : $"https://rollingplacesweb.azurewebsites.net{PicturePath.Substring(1)}";
        : LoginType == LoginType.RollingPlaces? $"https://rollingplacesweb.azurewebsites.net{PicturePath.Substring(1)}" : PicturePath;
    }
}
