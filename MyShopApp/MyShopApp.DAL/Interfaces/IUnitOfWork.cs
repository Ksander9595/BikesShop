using MyShopApp.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Motocycle> Motocycles { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
