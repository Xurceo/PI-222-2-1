using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        TModel GetById(int id);
        IEnumerable<TModel> GetAll();
        void Create(TModel model);
        void Update(TModel model);
        void Delete(int id);
        void Save();
    }
}
