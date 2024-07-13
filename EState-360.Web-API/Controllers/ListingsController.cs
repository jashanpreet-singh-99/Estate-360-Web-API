using Microsoft.AspNetCore.Mvc;
using EState_360.Core.Entities;
using EState_360.Core.Services;

namespace EState_360.Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly ILogger<ListingsController> _logger;
        private readonly ListingService _listingService;

        public ListingsController(ListingService listingService, ILogger<ListingsController> logger)
        {
            _listingService = listingService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listings = _listingService.GetAllListings();

            _logger.LogInformation("GET ALL: ", listings);

            return Ok(listings);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var listing = _listingService.GetListingById(id);
            if (listing == null)
            {
                return NotFound();
            }
            return Ok(listing);
        }

        [HttpPost]
        public IActionResult Create(Listing listing)
        {
            _listingService.AddListing(listing);

            _logger.LogInformation("CREATE:", listing);
            
            return CreatedAtAction(nameof(GetById), new { id = listing.Id }, listing);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }

            _logger.LogInformation("Update: ", id);

            _listingService.UpdateListing(listing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _listingService.DeleteListing(id);
            return NoContent();
        }
    }
}

