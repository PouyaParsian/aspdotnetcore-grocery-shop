using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyEshop.Models;

namespace MyEshop.Data;

public class MyEShopDbContext : DbContext
{
    public MyEShopDbContext(DbContextOptions<MyEShopDbContext> options): base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Category Seed
        modelBuilder.Entity<Category>().HasData(

            new Category { Id = 1, Name = "لبنیات", Description = "" },
            new Category { Id = 2, Name = "تنقلات", Description = "" },
            new Category { Id = 3, Name = "نوشیدنی", Description = "" },
            new Category { Id = 4, Name = "پروتئین", Description = "" }

        );
        #endregion


        #region Product Seed
        modelBuilder.Entity<Product>().HasData(

            new Product { Id = 1, Name = "پنیر خامه‌ای", Description = "پنیر خامه‌ای هراز - 300 گرمی" },
            new Product { Id = 2, Name = "شیر کم‌چرب", Description = "شیر کم‌چرب کاله - 200 میلی‌لیتری" },
            new Product { Id = 3, Name = "ماست پرچرب", Description = "ماست پرچرب دامداران - 900 گرم" },
            new Product { Id = 4, Name = "چیپس نمکی", Description = "چیپس نمکی مزمز - 65 گرمی" },
            new Product { Id = 5, Name = "کیک وانیلی", Description = "کیک وانیلی با کرم شکلات - 40 گرمی" },
            new Product { Id = 6, Name = "کشک خشک", Description = "کشک خشک تنقلاتی کشکام - 85 گرمی" },
            new Product { Id = 7, Name = "نوشابه پرتقالی", Description = "نوشابه پرتقالی فانتا - 1.5 لیتری" },
            new Product { Id = 8, Name = "نوشابه کوکا", Description = "نوشابه کولا کوکاکولا - 330 میلی‌لیتری" },
            new Product { Id = 9, Name = "آب آشامیدنی", Description = "آب معدنی دسانی - 500 میلی‌لیتری" },
            new Product { Id = 10, Name = "تخم مرغ", Description = "تخم مرغ - بسته 12 عددی" },
            new Product { Id = 11, Name = "فیله مرغ", Description = "فیله مرغ - 900 گرمی" },
            new Product { Id = 12, Name = "سوسیس آلمانی", Description = "سوسیس آلمانی - 1 کیلوگرمی" }

        );
        #endregion


        #region Item Seed
        modelBuilder.Entity<Item>().HasData(

            new Item { Id = 1, ProductId = 1, Price = 139500, QuantityInStock = 5 },
            new Item { Id = 2, ProductId = 2, Price = 29000, QuantityInStock = 8 },
            new Item { Id = 3, ProductId = 3, Price = 134000, QuantityInStock = 3 },
            new Item { Id = 4, ProductId = 4, Price = 54000, QuantityInStock = 6 },
            new Item { Id = 5, ProductId = 5, Price = 23800, QuantityInStock = 12 },
            new Item { Id = 6, ProductId = 6, Price = 122000, QuantityInStock = 4 },
            new Item { Id = 7, ProductId = 7, Price = 85000, QuantityInStock = 14 },
            new Item { Id = 8, ProductId = 8, Price = 64400, QuantityInStock = 10 },
            new Item { Id = 9, ProductId = 9, Price = 15000, QuantityInStock = 7 },
            new Item { Id = 10, ProductId = 10, Price = 126000, QuantityInStock = 11 },
            new Item { Id = 11, ProductId = 11, Price = 540000, QuantityInStock = 8 },
            new Item { Id = 12, ProductId = 12, Price = 585000, QuantityInStock = 5 }

        );
        #endregion

        #region Item Seed
        modelBuilder.Entity<User>().HasData(

            new User { Id = 1, Email = "admin@gmail.com", Password = "admin", RegisterDate = DateTime.Now, IsAdmin = true}
        );
        #endregion

        base.OnModelCreating(modelBuilder);
    }
}