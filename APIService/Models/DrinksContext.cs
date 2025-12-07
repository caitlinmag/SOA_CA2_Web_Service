using Microsoft.EntityFrameworkCore;
using APIService.Models;
using MongoDB.Driver;

namespace APIService.Models
{
    public class DrinksContext: DbContext
    {
        //public DbSet<DrinkItem> DrinkItems { get; set; } = null;
        //public DbSet<Supplier> Supplier { get; set; } = null!;
        //public DbSet<DrinksSales> DrinksSales { get; set; } = null!;
        //public string DbPath { get; }
        //public DbSet<DrinkItem> Drinks { get; set; } = null;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // seed database - drink item
        //    modelBuilder.Entity<DrinkItem>().HasData(
        //        new DrinkItem()
        //        {
        //            DrinkItemId = 1,
        //            DrinkName = "Black Americano",
        //            DrinkType = "Coffee",
        //            Price = 3.50,
        //            Extras = "None",
        //            SupplierId = 2,
        //            Supplier = new Supplier()
        //        });

        //    // Supplier data 
        //    modelBuilder.Entity<DrinkItem>().HasData(
        //       new Supplier()
        //       {
        //           SupplierId = 2,
        //           SupplierName = "Costa Coffee",
        //           Location = "Dundalk",
        //           StockLevel = 10,
        //           DrinkItems = new DrinkItem[] { }
        //       });

        //    // Drink sales data
        //    modelBuilder.Entity<DrinkItem>().HasData(
        //       new DrinksSales()
        //       {
        //           DrinksSalesId = 3,
        //           DrinkItem = new DrinkItem(),
        //           DrinkItemId = 1,
        //           Quantity = 20,
        //           DateOfSale = DateTime.UtcNow
        //       });
        //    base.OnModelCreating(modelBuilder);
        }
    }

