using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportiveOrder.Models;

namespace SportiveOrder.Context
{
    public class SportiveOrderContext:IdentityDbContext<AppUser>
    {


        public SportiveOrderContext(DbContextOptions<SportiveOrderContext> options):base(options)
        {

        }

        public SportiveOrderContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ENGIN;Database=SportiveeOrder;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Şirket Adres bire-bir ilişki 
            modelBuilder.Entity<Company>().HasOne<Address>(c => c.CompanyAddress).WithOne(ad => ad.Company).HasForeignKey<Address>(ad => ad.CompanyId).OnDelete(DeleteBehavior.Cascade);
            // Kullanıcı Şirket bire-bir ilişki
            modelBuilder.Entity<AppUser>().HasOne<Company>(u => u.UserCompany).WithOne(c => c.User).HasForeignKey<Company>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            // Kategori Ürün bire -çok ilişki
            modelBuilder.Entity<Product>().HasOne<Category>(p => p.Category).WithMany(c => c.Products).HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Cascade);
            // Kullanıcı Sipariş bir-Çok ilişki
            modelBuilder.Entity<Order>().HasOne<AppUser>(o => o.User).WithMany(u => u.Orders).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
            // Sipariş Ürün çoka-çok ilişki
            modelBuilder.Entity<OrderItems>().HasKey(oi => new { oi.OrderId, oi.ProductId, oi.Id });
            modelBuilder.Entity<OrderItems>().HasOne<Product>(oi => oi.Product).WithMany(p => p.OrderItems).HasForeignKey(oi => oi.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderItems>().HasOne<Order>(oi => oi.Order).WithMany(o => o.OrderItems).HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderItems>().HasIndex(I => new { I.OrderId, I.ProductId }).IsUnique();
            base.OnModelCreating(modelBuilder);

            
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
    }
}
