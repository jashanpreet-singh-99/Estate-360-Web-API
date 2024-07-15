using EState_360.Core.Entities;
using EState_360.Core.Repositories;
using EState_360.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EState_360.Infrastructure.Repositories
{
    public class CosmosMongoDbRepository: IListingRepository
    {
        private readonly IMongoCollection<Listing> _listings;
        private readonly ILogger<CosmosMongoDbRepository> _logger;

        private const int MAX_PER_PAGE = 6;

        public CosmosMongoDbRepository(
            MongoDbContext dbContext,
            ILogger<CosmosMongoDbRepository> logger
            )
        {
            _listings = dbContext.Listings;
            _logger = logger;
        }

        public async Task<IEnumerable<Listing>> GetAll()
        {
            // Placed limit to same resources.
            return await _listings.Find(Builders<Listing>.Filter.Empty).Limit(MAX_PER_PAGE).ToListAsync();
        }

        public async Task<Listing> GetById(string id)
        {
            return await _listings.Find(listing => listing.Id == id).FirstOrDefaultAsync();
        }

        public async Task Add(Listing listing)
        {
            listing.Id = ObjectId.GenerateNewId().ToString();
            await _listings.InsertOneAsync(listing);
        }

        public async Task Update(Listing listing)
        {
            await _listings.ReplaceOneAsync(l => l.Id == listing.Id, listing);
        }

        public async Task Delete(string id)
        {
            await _listings.DeleteOneAsync(listing => listing.Id == id);
        }

        public async Task<IEnumerable<Listing>> GetTopListings(int count)
        {
            return await _listings
                .Find(Builders<Listing>.Filter.Empty)
                .SortByDescending(p => p.Rating)
                .Limit(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Listing>> SearchListings(ListingSearch listingSearch)
        {
            var filter = CreateSearchFilter(listingSearch);
            //var filter = Builders<Listing>.Filter.Gt("price", 4000);
            return await _listings
                .Find(filter)
                .Limit(MAX_PER_PAGE)
                .ToListAsync();
        }

        private FilterDefinition<Listing> CreateSearchFilter(ListingSearch listingSearch)
        {
            var filterBuilder = Builders<Listing>.Filter;
            var filters = new List<FilterDefinition<Listing>>();

            // Type search
            if (!string.IsNullOrEmpty(listingSearch.Type))
            {
                _logger.LogInformation("Search Type: " + listingSearch.Type);

                var typeFilter = filterBuilder.Eq(l => l.Type, listingSearch.Type);
                filters.Add(typeFilter);
            }

            // Keyword search
            if (!string.IsNullOrEmpty(listingSearch.Keywords))
            {
                _logger.LogInformation("Search Keyword: " + listingSearch.Keywords);

                var keywordFilter = filterBuilder.Regex(l => l.Name, ConvertKeywordsToBsonRegex(listingSearch.Keywords));
                filters.Add(keywordFilter);
            }

            // Location search
            if (!string.IsNullOrEmpty(listingSearch.Location))
            {
                _logger.LogInformation("Search Location: " + listingSearch.Location);

                var locationFilter = filterBuilder.Eq(l => l.Region, listingSearch.Location);
                filters.Add(locationFilter);
            }

            // Price Range Check Min Price
            if (listingSearch.MinPrice.HasValue && listingSearch.MinPrice.Value > 0)
            {
                _logger.LogInformation("Search Min Price: " + listingSearch.MinPrice.Value);

                var minPriceFilter = filterBuilder.Gte(l => l.Price, listingSearch.MinPrice.Value);
                filters.Add(minPriceFilter);
            }

            // Price Range Check Max Price
            if (listingSearch.MaxPrice.HasValue && listingSearch.MaxPrice.Value > 0)
            {
                _logger.LogInformation("Search Max Price: " + listingSearch.MaxPrice.Value);

                var maxPriceFilter = filterBuilder.Lte(l => l.Price, listingSearch.MaxPrice.Value);
                filters.Add(maxPriceFilter);
            }

            if (filters.Count == 0)
            {
                return Builders<Listing>.Filter.Empty;
            }
            var combinedFilter = filterBuilder.And(filters);
            return combinedFilter;
        }

        private BsonRegularExpression ConvertKeywordsToBsonRegex(string keywordsText)
        {
            var keywords = keywordsText.Split(" ");
            var regexPattern = string.Join("|", keywords);

            _logger.LogInformation("Search Keywords Pattern: " + regexPattern);

            return new BsonRegularExpression(regexPattern, "i");
        }
    }
}
