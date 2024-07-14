using EState_360.Core.Entities;

namespace EState_360.Core.Repositories
{
    public interface IListingRepository
    {
        Task<IEnumerable<Listing>> GetAll();
        Task<Listing> GetById(string id);
        Task Add(Listing listing);
        Task Update(Listing listing);
        Task Delete(string id);
        Task<IEnumerable<Listing>> GetTopListings(int count);
    }
}