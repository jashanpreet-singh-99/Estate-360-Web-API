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

        public IEnumerable<Listing> GetAllListings()
        {
            return _listingRepository.GetAll();
        }

        public Listing GetListingById(int id)
        {
            return _listingRepository.GetById(id);
        }

        public void AddListing(Listing listing)
        {
            _listingRepository.Add(listing);
        }

        public void UpdateListing(Listing listing)
        {
            _listingRepository.Update(listing);
        }

        public void DeleteListing(int id)
        {
            _listingRepository.Delete(id);
        }
    }
}
