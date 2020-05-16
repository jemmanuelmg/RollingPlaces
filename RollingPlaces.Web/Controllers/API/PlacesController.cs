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
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        /*UserEntity userEntity = await _userHelper.GetUserAsync(qualificationsRequest.Qualifications.UserId);
        List<PlaceEntity> auxPlaceList = await _context.Places.Where(t => t.Id == qualificationsRequest.PlaceId).ToListAsync();
        QualificationEntity qualificationEntity = new QualificationEntity
        {
            Place = auxPlaceList[0],
            User = userEntity,
            Value = qualificationsRequest.Value,
            Comment=qualificationsRequest.Comment,
            CreatedDate = DateTime.UtcNow
        };

        _context.Qualifications.Add(qualificationEntity);
        await _context.SaveChangesAsync();
        return Ok(_converterHelper.ToQualificationResponse(qualificationEntity));
    }*/

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPlaceEntity([FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PlaceEntity placeEntity = await _context.Places
                .Include(t => t.User)
                .Include(t => t.City)
                .Include(t => t.Category)
                .Include(t => t.Photos).
                //FirstOrDefaultAsync(t => t.Name == name);
                FirstOrDefaultAsync(t => t.Name.Contains(name) == true);

            return Ok(placeEntity);
        }
    }
}
