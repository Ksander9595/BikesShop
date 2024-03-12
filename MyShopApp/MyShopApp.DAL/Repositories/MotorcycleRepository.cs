﻿using Microsoft.EntityFrameworkCore;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;


namespace MyShopApp.DAL.Repositories
{
    public class MotorcycleRepository : IRepository<Motorcycle>//в каждом методе SaveChangeAsync or общий в UnitOfWork
    {
        private ApplicationDbContext db;
        public MotorcycleRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public IEnumerable<Motorcycle> GetAll()
        {
            return db.Motorcycles;
        }
        public async Task<Motorcycle> GetAsync(int id)
        {
            return await db.Motorcycles.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateAsync(Motorcycle motorcycle)
        {
            await db.Motorcycles.AddAsync(motorcycle);
        }
        public async Task UpdateAsync(Motorcycle motorcycle)
        {
            db.Motorcycles.Update(motorcycle);
            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            if (id != null)
            {
                Motorcycle? motorcycle = await db.Motorcycles.FirstOrDefaultAsync(m => m.Id == id);
                db.Motorcycles.Remove(motorcycle);
            }
        }
        public async Task<IEnumerable<Motorcycle>> FindAsync(Func<Motorcycle, Boolean> predicate)
        {
            await db.SaveChangesAsync();
            return db.Motorcycles.Where(predicate).ToList();

        }

    }
    
}