﻿

namespace MyShopApp.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetAsync(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task CreateAsync(T item);
        void Update(T item);
        Task DeleteAsync(int id);
    }
}
