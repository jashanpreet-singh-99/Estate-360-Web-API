using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EState_360.Core.Entities;
using EState_360.Core.Services;
using EState_360.Web_API.DTOs;

namespace EState_360.Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly ILogger<ListingsController> _logger;
        private readonly ListingService _listingService;
        private readonly IMapper _mapper;

        public ListingsController(
            ListingService listingService,
            ILogger<ListingsController> logger,
            IMapper mapper)
        {
            _listingService = listingService;
            _logger = logger;
            _mapper = mapper;
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
        public async Task<IActionResult> Create([FromBody] ListingCreateDto listingDto)
        {
            // convert listingDto to listing object
            var listing = _mapper.Map<Listing>(listingDto);

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

        [HttpGet("top")]
        public async Task<IActionResult> GetTopListings([FromQuery] int? count = 6)
        {
            if (count > 10)
            {
                count = 10;
            }
            var topListings = await _listingService.GetTopListings(count ?? 6);
            return Ok(topListings);
        }
    }
}

