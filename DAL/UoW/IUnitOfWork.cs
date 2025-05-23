using DAL.Models;
using DAL.Repositories;

namespace DAL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Lot> Lots { get; }
        IGenericRepository<Bid> Bids { get; }
        IGenericRepository<Category> Categories { get; }

        void Save();
    }
}
