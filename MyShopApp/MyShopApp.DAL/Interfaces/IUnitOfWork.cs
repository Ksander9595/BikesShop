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
        IRepository<Motorcycle> Motorcycles { get; }
        IRepository<Order> Orders { get; }
        Task SaveAsync();
    }
}
