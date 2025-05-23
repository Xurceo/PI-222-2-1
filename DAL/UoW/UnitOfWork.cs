using DAL.Models;
using DAL.Repositories;

namespace DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuctionDbContext _context;

        private IGenericRepository<User>? _users;
        private IGenericRepository<Lot>? _lots;
        private IGenericRepository<Bid>? _bids;
        private IGenericRepository<Category>? _categories;

        public UnitOfWork(AuctionDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<User> Users =>
            _users ??= new GenericRepository<User>(_context);

        public IGenericRepository<Lot> Lots =>
            _lots ??= new GenericRepository<Lot>(_context);

        public IGenericRepository<Bid> Bids =>
            _bids ??= new GenericRepository<Bid>(_context);

        public IGenericRepository<Category> Categories =>
            _categories ??= new GenericRepository<Category>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
