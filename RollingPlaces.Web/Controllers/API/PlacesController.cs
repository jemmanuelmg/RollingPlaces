using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RollingPlaces.Web.Data;
using RollingPlaces.Web.Data.Entities;
using System.Linq;
using System.Collections.Generic;
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

        public PlacesController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        [Route("GetPlaces")]
        public async Task<IActionResult> GetPlaceEntity([FromBody] PlaceRequest placeRequest)
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
    }
}
