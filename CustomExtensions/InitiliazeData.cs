using Microsoft.EntityFrameworkCore;
using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.CustomExtensions
{
    public class InitiliazeData
    {
        public static void InitiliazeDatas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1,CategoryName ="Antreman" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Futbol" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Günlük Stil" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 4, CategoryName = "Koşu" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 5, CategoryName = "Tenis" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 6, CategoryName = "Voleybol" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 7, CategoryName = "Diğer" });
            modelBuilder.Entity<Product>().HasData(new Product {  ProductId= 1, Barcode = "0887791159038", CategoryId = 5, Color = "SİYAH", KDV = 18, Stock = 61, ProductGroup = "TOP", ProductName = "JORDAN SKILLS BLACK/WOLF GREY/GYM RED/GYM RED 03", PurchasePrice = (decimal)123.00, SalePrice = (decimal)863.00, Size = "56", StockCode = "J.00560.1884.0456.03" });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 2, Barcode = "0887791158918", CategoryId = 2, Color = "MVAİ", KDV = 18, Stock = 21, ProductGroup = "TOP", ProductName = "JORDAN SKILLS BLACK/WOLF GREY/GYM RED/GYM RED 01", PurchasePrice = (decimal)13.00, SalePrice = (decimal)333.00, Size = "7", StockCode = "J.0050.41.04561.03" });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 3,Barcode = "0887761155048", CategoryId = 3, Color = "KIRMIZI", KDV = 18, Stock = 41, ProductGroup = "SULUK", ProductName = "JORDAN SKILLS BLACK/WOLF GREY/GYM RED/GYM RED 02", PurchasePrice = (decimal)134.00, SalePrice = (decimal)483.00, Size = "2", StockCode = "J.05690.546.041.03" });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 4,Barcode = "0887751123538", CategoryId = 1, Color = "PEMBE", KDV = 18, Stock = 91, ProductGroup = "İP", ProductName = "JORDAN SKILLS TEST3", PurchasePrice = (decimal)11.00, SalePrice = (decimal)153.00, Size = "13", StockCode = "J.000.1884.041.03" });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 5, Barcode = "0887792559008", CategoryId = 3, Color = "YEŞİL", KDV = 18, Stock = 51, ProductGroup = "ELDİVEN", ProductName = "JORDAN SKILLS TEST1", PurchasePrice = (decimal)23.00, SalePrice = (decimal)283.00, Size = "3", StockCode = "J.000.1884.041.03" });
        }

    
    }
}
