﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RollingPlaces.Web.Data;
using RollingPlaces.Common.Models;
using RollingPlaces.Web.Data.Entities;
using RollingPlaces.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System;
using System.Linq;


namespace RollingPlaces.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public PlacesController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper, IImageHelper imageHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("AddQualification")]
        public async Task<IActionResult> AddQualification([FromBody] QualificationsRequest qualificationsRequest)
        {
           
            PlaceEntity place = await _context.Places
                .Include(t => t.Qualifications)
                .FirstOrDefaultAsync(t => t.Id == qualificationsRequest.Qualifications.FirstOrDefault().PlaceId);

            if (place == null)  
            {
                return BadRequest("Place not found.");
            }
            if (place.Qualifications == null)
            {
                place.Qualifications = new List<QualificationEntity>();
            }
           
            foreach (QualificationRequest qualificationRequest in qualificationsRequest.Qualifications)
            {
                UserEntity userEntity = await _userHelper.GetUserAsync(qualificationRequest.UserId);
                place.Qualifications.Add(new QualificationEntity
                {
                    User=userEntity,
                    Value = qualificationRequest.Value,
                    Comment = qualificationRequest.Comment,
                    CreatedDate = DateTime.UtcNow
                });
            }

            _context.Places.Update(place);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [Route("GetPlaces")]
        public async Task<IActionResult> GetPlaces([FromBody] PlaceRequest placeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var placeEntities = await _context.Places.ToListAsync();

            if (string.IsNullOrEmpty(placeRequest.Keywords) && placeRequest.CategoryId == 777)
            {
                placeEntities = await _context.Places
                .Where(t => t.City.Id == placeRequest.CityId)
                .Include(t => t.User)
                .Include(t => t.City)
                .Include(t => t.Category)
                .Include(t => t.Qualifications)
                .Include(t => t.Photos)
                .ToListAsync();

            }
            else if (!string.IsNullOrEmpty(placeRequest.Keywords) && placeRequest.CategoryId != 777)
            {
                placeEntities = await _context.Places
               .Where(t => t.City.Id == placeRequest.CityId)
               .Where(t => t.Category.Id == placeRequest.CategoryId)
               .Where(t => t.Name.Contains(placeRequest.Keywords) == true)
               .Include(t => t.User)
               .Include(t => t.City)
               .Include(t => t.Category)
               .Include(t => t.Qualifications)
               .Include(t => t.Photos)
               .ToListAsync();
            }
            else if (string.IsNullOrEmpty(placeRequest.Keywords) && placeRequest.CategoryId != 777)
            {
                placeEntities = await _context.Places
               .Where(t => t.City.Id == placeRequest.CityId)
               .Where(t => t.Category.Id == placeRequest.CategoryId)
               .Include(t => t.User)
               .Include(t => t.City)
               .Include(t => t.Category)
               .Include(t => t.Qualifications)
               .Include(t => t.Photos)
               .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(placeRequest.Keywords) && placeRequest.CategoryId == 777)
            {
                placeEntities = await _context.Places
               .Where(t => t.City.Id == placeRequest.CityId)
               .Where(t => t.Name.Contains(placeRequest.Keywords) == true)
               .Include(t => t.User)
               .Include(t => t.City)
               .Include(t => t.Category)
               .Include(t => t.Qualifications)
               .Include(t => t.Photos)
               .ToListAsync();
            }

            List<PlaceResponse> placesList = new List<PlaceResponse>();
            foreach (PlaceEntity element in placeEntities)
            {
                placesList.Add(_converterHelper.ToPlaceResponse(element));
            }

            return Ok(placesList);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostPlaceEntity([FromBody] PlaceRequest2 placeRequest2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            UserEntity userEntity = await _userHelper.GetUserAsync(placeRequest2.User);
            if (userEntity == null)
            {
                return BadRequest("User doesn't exists.");
            }

            List<CityEntity> auxCityList = await _context.Cities.Where(t => t.Id == placeRequest2.CityId).ToListAsync();
            List<CategoryEntity> auxCategoryList = await _context.Categories.Where(t => t.Id == placeRequest2.CategoryId).ToListAsync();

            PlaceEntity placeEntity = new PlaceEntity
            {
                Description = placeRequest2.Description,
                Latitude = Math.Round((Double)placeRequest2.Latitude, 6),
                Longitude = Math.Round((Double)placeRequest2.Longitude, 6),
                Name = placeRequest2.Name,
                User = userEntity,
                City = auxCityList[0],
                Category = auxCategoryList[0],
                CreatedDate = DateTime.Now
            };

            _context.Places.Add(placeEntity);
            await _context.SaveChangesAsync();

            List<PlaceEntity> auxPlaceList = await _context.Places
                .Where(t => t.Name.Contains(placeRequest2.Name) == true)
                .Where(t => t.User.Id == placeRequest2.User.ToString())
                .ToListAsync();

            string photoPath1 = string.Empty;
            if (placeRequest2.PictureArray1 != null && placeRequest2.PictureArray1.Length > 0)
            {
                photoPath1 = _imageHelper.UploadImage(placeRequest2.PictureArray1, "Places");
                PhotoEntity photo1 = new PhotoEntity();
                photo1.PhotoPath = photoPath1;
                photo1.Place = auxPlaceList[0];
                _context.Photos.Add(photo1);
            }

            string photoPath2 = string.Empty;
            if (placeRequest2.PictureArray2 != null && placeRequest2.PictureArray2.Length > 0)
            {
                photoPath2 = _imageHelper.UploadImage(placeRequest2.PictureArray2, "Places");
                PhotoEntity photo2 = new PhotoEntity();
                photo2.PhotoPath = photoPath2;
                photo2.Place = auxPlaceList[0];
                _context.Photos.Add(photo2);
            }

            string photoPath3 = string.Empty;
            if (placeRequest2.PictureArray3 != null && placeRequest2.PictureArray3.Length > 0)
            {
                photoPath3 = _imageHelper.UploadImage(placeRequest2.PictureArray3, "Places");
                PhotoEntity photo3 = new PhotoEntity();
                photo3.PhotoPath = photoPath3;
                photo3.Place = auxPlaceList[0];
                _context.Photos.Add(photo3);
            }

            string photoPath4 = string.Empty;
            if (placeRequest2.PictureArray4 != null && placeRequest2.PictureArray4.Length > 0)
            {
                photoPath4 = _imageHelper.UploadImage(placeRequest2.PictureArray4, "Places");
                PhotoEntity photo4 = new PhotoEntity();
                photo4.PhotoPath = photoPath4;
                photo4.Place = auxPlaceList[0];
                _context.Photos.Add(photo4);
            }

            await _context.SaveChangesAsync();

            return Ok(_converterHelper.ToPlaceResponse(placeEntity));
        }

    }
}
