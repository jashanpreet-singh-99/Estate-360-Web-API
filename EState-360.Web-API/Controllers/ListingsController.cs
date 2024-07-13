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
        public async Task<IActionResult> GetAll()
        {
            var listings = await _listingService.GetAllListings();

            _logger.LogInformation("GET ALL: ", listings);

            return Ok(listings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var listing = await _listingService.GetListingById(id);
            if (listing == null)
            {
                return NotFound();
            }
            return Ok(listing);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Listing listing)
        {
            await _listingService.AddListing(listing);

            _logger.LogInformation("CREATE:", listing);
            
            return CreatedAtAction(nameof(GetById), new { id = listing.Id }, listing);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }

            _logger.LogInformation("Update: ", id);

            await _listingService.UpdateListing(listing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _listingService.DeleteListing(id);
            return NoContent();
        }
    }
}

