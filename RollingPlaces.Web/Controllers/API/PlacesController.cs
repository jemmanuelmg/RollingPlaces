using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RollingPlaces.Web.Data;
using RollingPlaces.Web.Data.Entities;

namespace RollingPlaces.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly DataContext _context;

        public PlacesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPlaceEntity([FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            PlaceEntity placeEntity = await _context.Places.FirstOrDefaultAsync(t => t.Name == name);

            if (placeEntity == null)
            {
                 _context.Places.Add(new PlaceEntity { Name = name });
                 await _context.SaveChangesAsync();
                 placeEntity = await _context.Places.FirstOrDefaultAsync(t => t.Name == name);              
            }

            return Ok(placeEntity);
        }
    }
}
