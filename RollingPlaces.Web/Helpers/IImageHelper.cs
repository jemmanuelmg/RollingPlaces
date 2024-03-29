﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RollingPlaces.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);

        string UploadImage(byte[] pictureArray, string folder);

    }
}
