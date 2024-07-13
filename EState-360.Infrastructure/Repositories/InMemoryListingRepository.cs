using EState_360.Core.Entities;
using EState_360.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace EState_360.Infrastructure.Repositories
{
    public class InMemoryListingRepository : IListingRepository
    {
        private readonly List<Listing> _listings = new List<Listing>();

        public IEnumerable<Listing> GetAll()
        {
            return _listings;
        }

        public Listing GetById(int id)
        {
            return _listings.FirstOrDefault(l => l.Id == id) ?? new Listing();
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

        public void Delete(int id)
        {
            var listing = GetById(id);
            if (listing != null)
            {
                _listings.Remove(listing);
            }
        }
    }
}

