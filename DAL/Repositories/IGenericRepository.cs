using System.Linq.Expressions;

namespace DAL.Repositories
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        TModel GetById(Guid id);
        IEnumerable<TModel> GetAll(params Expression<Func<TModel, object>>[] includeProperties);
        void Create(TModel model);
        void Update(TModel model);
        void Delete(Guid id);
        void Save();
        bool Exists(Guid value);
    }
}
