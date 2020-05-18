using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RollingPlaces.Web.Data;
using RollingPlaces.Web.Data.Entities;
using RollingPlaces.Common.Models;
using RollingPlaces.Web.Helpers;

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

        // POST: api/Travels
        [HttpPost]
        public async Task<IActionResult> PostPlaceEntity([FromBody] PlaceRequest placeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserEntity userEntity = await _userHelper.GetUserByEmailAsync(placeRequest.User);
            if (userEntity == null)
            {
                return BadRequest("User doesn't exists.");
            }

            PlaceEntity placeEntity = new PlaceEntity
            {
                Description = placeRequest.Description,
                Latitude = placeRequest.Latitude,
                Longitude = placeRequest.Longitude,
                Name = placeRequest.Name,
                User = userEntity
                /*TripDetails = new List<TripDetailsEntity>
                {
                     new TripDetailsEntity
                    {
                        Origin = tripRequest.Origin,
                        Description= tripRequest.Description,

                    }
                }*/
            };

            _context.Places.Add(placeEntity);
            await _context.SaveChangesAsync();

            return Ok(_converterHelper.ToPlaceResponse(placeEntity)); ;
        }

    }
}
