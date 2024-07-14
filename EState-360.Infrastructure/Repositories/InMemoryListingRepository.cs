using EState_360.Core.Entities;

namespace EState_360.Infrastructure.Repositories
{
    public class InMemoryListingRepository
    {
        private readonly List<Listing> _listings = new();

        public IEnumerable<Listing> GetAll()
        {
            return _listings;
        }

        public Listing GetById(string id)
        {
            return _listings.FirstOrDefault(l => l.Id == id) ?? new Listing { Id = "-1", Name = "", Region = "", Type="Sell"};
        }

        public void Add(Listing listing)
        {
            _listings.Add(listing);
        }

        public void Update(Listing listing)
        {
            var listingProduct = GetById(listing.Id);
            if (listingProduct != null)
            {
                listingProduct.Name = listing.Name;
                listingProduct.Price = listing.Price;
            }
        }

        public void Delete(string id)
        {
            var listing = GetById(id);
            if (listing != null)
            {
                _listings.Remove(listing);
            }
        }
    }
}

