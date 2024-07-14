using EState_360.Core.Entities;
using EState_360.Core.Repositories;
using EState_360.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EState_360.Infrastructure.Repositories
{
    public class CosmosMongoDbRepository: IListingRepository
    {
        private readonly IMongoCollection<Listing> _listings;

        public CosmosMongoDbRepository(MongoDbContext dbContext)
        {
            _listings = dbContext.Listings;
        }

        public async Task<IEnumerable<Listing>> GetAll()
        {
            return await _listings.Find(listing => true).ToListAsync();
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
    }
}
