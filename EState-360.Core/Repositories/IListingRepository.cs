using EState_360.Core.Entities;

namespace EState_360.Core.Repositories
{
    public interface IListingRepository
    {
        IEnumerable<Listing> GetAll();
        Listing GetById(int id);
        void Add(Listing listing);
        void Update(Listing listing);
        void Delete(int id);
    }
}