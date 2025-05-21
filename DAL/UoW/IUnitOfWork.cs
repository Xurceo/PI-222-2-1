using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
