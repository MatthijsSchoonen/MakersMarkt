
using BCrypt.Net;
using MakersMarkt.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace MakersMarkt.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ProductSpecs> ProductSpecs { get; set; }
        public DbSet<Complexity> Complexities { get; set; }
        public DbSet<Durability> Durabilities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;" +
                "port=3306;" +
                "user=root;" +
                "password=;" +
                "database=csd_makersmarkt",
                ServerVersion.Parse("10.4.17-mariadb")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "user1", Password = BCrypt.Net.BCrypt.EnhancedHashPassword("123"), Email = "user1@example.com", Balance = 100, RoleId = 1, AllowEmails = true, IsVerified = true, LoginAttempts = 0, Rating = 4 },
                new User { Id = 2, Username = "user2", Password = BCrypt.Net.BCrypt.EnhancedHashPassword("123"), Email = "user2@example.com", Balance = 200, RoleId = 2, AllowEmails = false, IsVerified = false, LoginAttempts = 0, Rating = 0}
            );

            modelBuilder.Entity<UserNotification>().HasData(
                new UserNotification { Id = 1, UserId = 1, Title = "Notification 1", Text = "This is the first notification." },
                new UserNotification { Id = 2, UserId = 2, Title = "Notification 2", Text = "This is the second notification." }
            );

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Type1" },
                new ProductType { Id = 2, Name = "Type2" }
            );

            modelBuilder.Entity<Material>().HasData(
                new Material { Id = 1, Name = "Material1", Amount = 10 },
                new Material { Id = 2, Name = "Material2", Amount = 20 }
            );

            modelBuilder.Entity<ProductSpecs>().HasData(
                new ProductSpecs { Id = 1, ProductId = 1, MaterialId = 1, ProductionTime = 5, ComplexityId = 1, DurabilityId = 1 },
                new ProductSpecs { Id = 2, ProductId = 2, MaterialId = 2, ProductionTime = 10, ComplexityId = 2, DurabilityId = 2 }
            );

            modelBuilder.Entity<Complexity>().HasData(
                new Complexity { Id = 1, Name = "Simple" },
                new Complexity { Id = 2, Name = "Complex" }
            );

            modelBuilder.Entity<Durability>().HasData(
                new Durability { Id = 1, Name = "Low", LifeTime = 1, Description = "Low durability" },
                new Durability { Id = 2, Name = "High", LifeTime = 5, Description = "High durability" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product1", Description = "Description1", ProductTypeId = 1, UserId = 1, Price = 10.0m, IsFlagged = false, Reports = 0 },
                new Product { Id = 2, Name = "Product2", Description = "Description2", ProductTypeId = 2, UserId = 2, Price = 20.0m, IsFlagged = true, Reports = 1 }
            );

            modelBuilder.Entity<PasswordReset>().HasData(
                new PasswordReset { Id = 1, UserId = 1, Code = "code1", CreatedAt = DateTime.Now },
                new PasswordReset { Id = 2, UserId = 2, Code = "code2", CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "In Production" },
                new Status { Id = 2, Name = "Sent" },
                new Status { Id = 3, Name = "Declined, refund sent." }
            );
           
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, Description = "Order1", ProductId = 1, StatusId = 1, BuyerId = 1, SellerId = 2 },
                new Order { Id = 2, Description = "Order2", ProductId = 2, StatusId = 2, BuyerId = 2, SellerId = 1 }
            );

            modelBuilder.Entity<ProductProperty>().HasData(
                new ProductProperty { Id = 1, ProductId = 1, Name = "Property1", Value = "Value1" },
                new ProductProperty { Id = 2, ProductId = 2, Name = "Property2", Value = "Value2" }
            );
        }
    }
}
