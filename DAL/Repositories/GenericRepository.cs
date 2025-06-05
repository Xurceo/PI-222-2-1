using DAL.Models;
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

        public async Task<TModel?> GetById(Guid id)
        {
            IQueryable<TModel> query = _dbSet;

            var navigationProperties = _context.Model.FindEntityType(typeof(TModel))?
                .GetNavigations()
                .Select(n => n.Name);

            if (navigationProperties != null)
            {
                foreach (var navProperty in navigationProperties)
                {
                    query = query.Include(navProperty);
                }
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<IEnumerable<TModel>> GetAll(params Expression<Func<TModel, object>>[] includeProperties)
        {
            IQueryable<TModel> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TModel>> GetAll()
        {

            IQueryable<TModel> query = _dbSet;

            var navigationProperties = _context.Model.FindEntityType(typeof(TModel))?
                .GetNavigations()
                .Select(n => n.Name);

            if (navigationProperties != null)
            {
                foreach (var navProperty in navigationProperties)
                {
                    query = query.Include(navProperty);
                }
            }

            return await query.ToListAsync();
        }

        public async Task Create(TModel model)
        {
            await _dbSet.AddAsync(model);
        }

        public Task Update(TModel model)
        {
            _dbSet.Update(model);
            return Task.CompletedTask;
        }

        public void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
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
