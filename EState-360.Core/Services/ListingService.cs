using EState_360.Core.Entities;
using EState_360.Core.Repositories;

namespace EState_360.Core.Services {
    public class ListingService
    {
        private readonly IListingRepository _listingRepository;

        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public Task<IEnumerable<Listing>> GetAllListings()
        {
            return _listingRepository.GetAll();
        }

        public Task<Listing> GetListingById(string id)
        {
            return _listingRepository.GetById(id);
        }

        public Task AddListing(Listing listing)
        {
            listing.PostedOn = DateTime.UtcNow;


            return _listingRepository.Add(listing);
        }

        public Task UpdateListing(Listing listing)
        {
            return _listingRepository.Update(listing);
        }

        public Task DeleteListing(string id)
        {
            return _listingRepository.Delete(id);
        }

        public Task<IEnumerable<Listing>> GetTopListings(int count)
        {
            return _listingRepository.GetTopListings(count);
        }
    }
}
