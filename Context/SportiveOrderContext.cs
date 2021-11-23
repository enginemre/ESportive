using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Context
{
    public class SportiveOrderContext:IdentityDbContext<AppUser>
    {
        public SportiveOrderContext(DbContextOptions<SportiveOrderContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Şirket Adres bire-bir ilişki 
            modelBuilder.Entity<Company>().HasOne<Address>(c => c.CompanyAddress).WithOne(ad => ad.Company).HasForeignKey<Address>(ad => ad.CompanyId);
            // Kullanıcı Şirket bire-bir ilişki
            modelBuilder.Entity<AppUser>().HasOne<Company>(u => u.UserCompany).WithOne(c => c.User).HasForeignKey<Company>(c => c.UserId);
            // Kategori Ürün bire -çok ilişki
            modelBuilder.Entity<Product>().HasOne<Category>(p => p.Category).WithMany(c => c.Products).HasForeignKey(c => c.CategoryId);
            // Kullanıcı Sipariş bir-Çok ilişki
            modelBuilder.Entity<Order>().HasOne<AppUser>(o => o.User).WithMany(u => u.Orders).HasForeignKey(u => u.UserId);
            // Sipariş Ürün çoka-çok ilişki
            modelBuilder.Entity<OrderItems>().HasKey(oi => new { oi.OrderId, oi.ProductId });
            modelBuilder.Entity<OrderItems>().HasOne<Product>(oi => oi.Product).WithMany(p => p.OrderItems).HasForeignKey(oi => oi.ProductId);
            modelBuilder.Entity<OrderItems>().HasOne<Order>(oi => oi.Order).WithMany(o => o.OrderItems).HasForeignKey(oi => oi.OrderId);
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
