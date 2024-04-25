using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShopApp.DAL.EF.Entities;

namespace MyShopApp.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
      
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Motorcycle> Motorcycles { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<ClientProfile> ClientProfiles { get; set; } = null!;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Motorcycle>().HasData(
                new Motorcycle {
                    Id = 1,
                    Name = "Harley-Davidson",
                    Model = "FatBoy",
                    Description = "Wide tires and stylish square headlight",
                    Price = 2015000,
                    motoClass = MotorcycleClass.Cruise,
                    Year = 2023,
                    Hp = 90,
                    Capacity = 1860,
                    Color = "Black",
                    Document = true,
                    Mileage = 0,
                    Condition = true,
                    Availability = true },
                new Motorcycle
                {
                    Id = 2,
                    Name = "Harley-Davidson",
                    Model = "SportsterS",
                    Description = "Maximum power and stunning style",
                    Price = 2322000,
                    motoClass = MotorcycleClass.Sport,
                    Year = 2023,
                    Hp = 121,
                    Capacity = 1250,
                    Color = "Grey",
                    Document = true,
                    Mileage = 0,
                    Condition = true,
                    Availability = true
                },
                new Motorcycle
                {
                    Id = 3,
                    Name = "Ducati",
                    Model = "Streetfighter",
                    Description = "Sport bike with a wide and high handlebar without guards",
                    Price = 1760000,
                    motoClass = MotorcycleClass.Sport,
                    Year = 2023,
                    Hp = 208,
                    Capacity = 1100,
                    Color = "Red",
                    Document = true,
                    Mileage = 0,
                    Condition = true,
                    Availability = true
                },
                new Motorcycle
                {
                    Id = 4,
                    Name = "Ducati",
                    Model = "Monster",
                    Description = "Standart and naked bike",
                    Price = 363000,
                    motoClass = MotorcycleClass.Classic,
                    Year = 2014,
                    Hp = 128,
                    Capacity = 1200,
                    Color = "Yellow",
                    Document = true,
                    Mileage = 7900,
                    Condition = true,
                    Availability = true
                },
                 new Motorcycle
                 {
                     Id = 5,
                     Name = "Honda",
                     Model = "Shadow",
                     Description = "Classic, imperious, not expensive cruiser",
                     Price = 263000,
                     motoClass = MotorcycleClass.Cruise,
                     Year = 2003,
                     Hp = 33,
                     Capacity = 400,
                     Color = "Black",
                     Document = true,
                     Mileage = 43000,
                     Condition = true,
                     Availability = true
                 },
                 new Motorcycle
                 {
                     Id = 6,
                     Name = "Honda",
                     Model = "CBF600",
                     Description = "Safe and attractive bike for everyone",
                     Price = 565000,
                     motoClass = MotorcycleClass.Classic,
                     Year = 2008,
                     Hp = 78,
                     Capacity = 600,
                     Color = "Grey",
                     Document = true,
                     Mileage = 37800,
                     Condition = true,
                     Availability = true
                 },
                 new Motorcycle
                 {
                     Id = 7,
                     Name = "Kawasaki",
                     Model = "Ninja",
                     Description = "One of the fastest and most powerful motorcycle",
                     Price = 930000,
                     motoClass = MotorcycleClass.Sport,
                     Year = 2019,
                     Hp = 142,
                     Capacity = 1000,
                     Color = "Red",
                     Document = true,
                     Mileage = 10900,
                     Condition = true,
                     Availability = true
                 },
                 new Motorcycle
                 {
                     Id = 8,
                     Name = "Kawasaki",
                     Model = "KX250",
                     Description = "Popular, reliable cross motocycle",
                     Price = 960000,
                     motoClass = MotorcycleClass.Cross,
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

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "user", NormalizedName = "USER".ToUpper() },
                new Role { Id = 2, Name = "moder", NormalizedName = "MODER".ToUpper() },
                new Role { Id = 3,  Name = "admin", NormalizedName = "ADMIN".ToUpper()}
                );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 3,
                    UserName = "admin@mail.ru",
                    Email = "admin@mail.ru",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "Qwerty123!")
                }
                );

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 3,
                    UserId = 3,
                }
                );
        }
    }
}
