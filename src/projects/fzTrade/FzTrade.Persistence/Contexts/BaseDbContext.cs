using Core.Security.Entities;
using FzTrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.FirstName).HasColumnName("FirstName");
                a.Property(p => p.LastName).HasColumnName("LastName");
                a.Property(p => p.Email).HasColumnName("Email");
                a.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                a.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(p => p.Status).HasColumnName("Status");
                a.HasMany(p => p.UserOperationClaims);
                a.HasMany(p => p.RefreshTokens);
            });
            modelBuilder.Entity<Customer>(a =>
            {
                a.ToTable("Customers");
                a.Property(p => p.Balance).HasColumnName("Balance");
                a.Property(p => p.Gender).HasColumnName("Gender");
                a.Property(p => p.Address).HasColumnName("Address");
                
            });
            modelBuilder.Entity<Supplier>(a =>
            {
                a.ToTable("Suppliers");
                a.Property(p => p.CompanyName).HasColumnName("Balance");
                a.Property(p => p.Location).HasColumnName("Location");
                a.Property(p => p.NumberOfProducts).HasColumnName("NumberOfProducts");
                a.HasMany(p => p.Products);

            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");

            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
                a.HasOne(p => p.User);
                a.HasOne(p => p.OperationClaim);

            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
                a.HasOne(p => p.User);
                a.HasOne(p => p.OperationClaim);

            });

            modelBuilder.Entity<RefreshToken>(a =>
            {
                a.ToTable("RefreshTokens").HasKey(k => k.Id);
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.Token).HasColumnName("Token");
                a.Property(p => p.Expires).HasColumnName("Expires");
                a.Property(p => p.Created).HasColumnName("Created");
                a.Property(p => p.CreatedByIp).HasColumnName("CreatedByIp");
                a.Property(p => p.Revoked).HasColumnName("Revoked");
                a.Property(p => p.RevokedByIp).HasColumnName("RevokedByIp");
                a.Property(p => p.ReplacedByToken).HasColumnName("ReplacedByToken");
                a.Property(p => p.ReasonRevoked).HasColumnName("ReasonRevoked");
                a.HasOne(p => p.User);
            });

            modelBuilder.Entity<Category>(a =>
            {
                a.ToTable("Categories").HasKey(k => k.Id);
                a.Property(p=>p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Subcategories);
            });

            modelBuilder.Entity<Subcategory>(a =>
            {
                a.ToTable("Subcategories").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.CategoryId).HasColumnName("CategoryId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasOne(p => p.Category);
                a.HasMany(p => p.Products);
            });

            modelBuilder.Entity<Product>(a =>
            {
                a.ToTable("Products").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.SupplierId).HasColumnName("SupplierId");
                a.Property(p => p.SubcategoryId).HasColumnName("SubcategoryId");
                a.Property(p => p.Size).HasColumnName("Size");
                a.Property(p => p.Price).HasColumnName("Price");
                a.Property(p => p.Description).HasColumnName("Description");
                a.Property(p => p.Stock).HasColumnName("Stock");
                a.HasOne(p => p.Subcategory);
                a.HasOne(p => p.Supplier);
            });

            modelBuilder.Entity<Order>(a =>
            {
                a.ToTable("Orders").HasKey(k=>k.Id);
                a.Property(p=>p.Id).HasColumnName("Id");
                a.Property(p => p.CustomerId).HasColumnName("CustomerId");
                a.Property(p => p.ProductId).HasColumnName("ProductId");
                a.Property(p => p.OrderDate).HasColumnName("OrderDate");
                a.HasMany(p => p.Products);
                a.HasOne(p => p.Customer);
            });

            OperationClaim[] operationClaimSeeds = { new(1, "User"), new(2, "Admin") };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimSeeds);
        }
    }
}
