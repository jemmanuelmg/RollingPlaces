using Microsoft.AspNetCore.Mvc;
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

        // POST: api/Travels
        [HttpPost]
        public async Task<IActionResult> PostPlaceEntity([FromBody] PlaceRequest2 placeRequest2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserEntity userEntity = await _userHelper.GetUserByEmailAsync(placeRequest2.User);
            if (userEntity == null)
            {
                return BadRequest("User doesn't exists.");
            }

            PlaceEntity placeEntity = new PlaceEntity
            {
                Description = placeRequest2.Description,
                Latitude = placeRequest2.Latitude,
                Longitude = placeRequest2.Longitude,
                Name = placeRequest2.Name,
                User = userEntity,
                City = new CityEntity
                {                 
                        Id = 1
                }
            };

            _context.Places.Add(placeEntity);
            await _context.SaveChangesAsync();

            return Ok(_converterHelper.ToPlaceResponse(placeEntity)); ;
        }

    }
}
