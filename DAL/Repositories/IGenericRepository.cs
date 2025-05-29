using System.Linq.Expressions;

namespace DAL.Repositories
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel?> GetById(Guid id);
        Task<IEnumerable<TModel>> GetAll(params Expression<Func<TModel, object>>[] includeProperties);
        Task Create(TModel model);
        Task Update(TModel model);
        void Delete(Guid id);
        Task Save();
        Task<bool> Exists(Guid value);
    }
}
