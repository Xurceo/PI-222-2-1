using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        protected readonly AuctionDbContext _context;
        protected readonly DbSet<TModel> _dbSet;

        public GenericRepository(AuctionDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TModel>();
        }

        public TModel GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TModel> GetAll(params Expression<Func<TModel, object>>[] includeProperties)
        {
            IQueryable<TModel> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public void Create(TModel model)
        {
            _dbSet.Add(model);
        }

        public void Update(TModel model)
        {
            _dbSet.Update(model);
        }

        public void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public bool Exists(Guid id)
        {
            return _dbSet.Any(e => EF.Property<Guid>(e, "Id") == id);
        }
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Помилка при збереженні змін:");
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
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
