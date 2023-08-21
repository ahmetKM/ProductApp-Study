using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProductApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Data.DbContexts
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> User {get; set;} 

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(x => x.Price).IsRequired().HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Product>().Property(x => x.Stock).IsRequired();


            modelBuilder.Entity<Product>().HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);

        }
    }
}
