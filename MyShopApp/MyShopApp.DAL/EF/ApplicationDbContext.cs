using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShopApp.DAL.EF.Entities;

namespace MyShopApp.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Motocycle> Motocycles { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motocycle>().HasData(
                new Motocycle {
                    Id = 1,
                    Name = "Harley-Davidson",
                    Model = "FatBoy",
                    Description = "Wide tires and stylish square headlight",
                    Price = 20150,
                    motoClass = MotocycleClass.Cruise,
                    Year = 2023,
                    Hp = 90,
                    Capacity = 1860,
                    Color = "Black",
                    Document = true,
                    Mileage = 0,
                    Condition = true,
                    Availability = true },
                new Motocycle
                {
                    Id = 2,
                    Name = "Harley-Davidson",
                    Model = "SportsterS",
                    Description = "Maximum power and stunning style",
                    Price = 23220,
                    motoClass = MotocycleClass.Sport,
                    Year = 2023,
                    Hp = 121,
                    Capacity = 1250,
                    Color = "Grey",
                    Document = true,
                    Mileage = 0,
                    Condition = true,
                    Availability = true
                },
                new Motocycle
                {
                    Id = 3,
                    Name = "Ducati",
                    Model = "Streetfighter",
                    Description = "Sport bike with a wide and high handlebar without guards",
                    Price = 17600,
                    motoClass = MotocycleClass.Sport,
                    Year = 2023,
                    Hp = 208,
                    Capacity = 1100,
                    Color = "Red",
                    Document = true,
                    Mileage = 0,
                    Condition = true,
                    Availability = true
                },
                new Motocycle
                {
                    Id = 4,
                    Name = "Ducati",
                    Model = "Monster",
                    Description = "Standart and naked bike",
                    Price = 3630,
                    motoClass = MotocycleClass.Classic,
                    Year = 2014,
                    Hp = 128,
                    Capacity = 1200,
                    Color = "Yellow",
                    Document = true,
                    Mileage = 7900,
                    Condition = true,
                    Availability = true
                },
                 new Motocycle
                 {
                     Id = 5,
                     Name = "Honda",
                     Model = "Shadow",
                     Description = "Classic, imperious, not expensive cruiser",
                     Price = 2630,
                     motoClass = MotocycleClass.Cruise,
                     Year = 2003,
                     Hp = 33,
                     Capacity = 400,
                     Color = "Black",
                     Document = true,
                     Mileage = 43000,
                     Condition = true,
                     Availability = true
                 },
                 new Motocycle
                 {
                     Id = 6,
                     Name = "Honda",
                     Model = "CBF600",
                     Description = "Safe and attractive bike for everyone",
                     Price = 5650,
                     motoClass = MotocycleClass.Classic,
                     Year = 2008,
                     Hp = 78,
                     Capacity = 600,
                     Color = "Grey",
                     Document = true,
                     Mileage = 37800,
                     Condition = true,
                     Availability = true
                 },
                 new Motocycle
                 {
                     Id = 7,
                     Name = "Kawasaki",
                     Model = "Ninja",
                     Description = "One of the fastest and most powerful motocycle",
                     Price = 9300,
                     motoClass = MotocycleClass.Sport,
                     Year = 2019,
                     Hp = 142,
                     Capacity = 1000,
                     Color = "Red",
                     Document = true,
                     Mileage = 10900,
                     Condition = true,
                     Availability = true
                 },
                 new Motocycle
                 {
                     Id = 8,
                     Name = "Kawasaki",
                     Model = "KX250",
                     Description = "Popular, reliable cross motocycle",
                     Price = 9600,
                     motoClass = MotocycleClass.Cross,
                     Year = 2022,
                     Hp = 39,
                     Capacity = 250,
                     Color = "Green",
                     Document = true,
                     Mileage = 0,
                     Condition = true,
                     Availability = true
                 }
                );
        }
    }
}
