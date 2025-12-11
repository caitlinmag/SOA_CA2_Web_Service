using APIService.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using APIService.Models;
using System.Formats.Asn1;
using Microsoft.AspNetCore.Mvc;
using APIService.Interfaces;

namespace APIService.Data
{
    public class DrinksService: IDrinksService
    {
        private readonly IMongoCollection<DrinkItem> _drinksCollection;
        private readonly IMongoCollection<Supplier> _supplierCollection;

        public DrinksService(IMongoCollection<DrinkItem> drinksCollection, IMongoCollection<Supplier> supplierCollection) 
        {
                _drinksCollection = drinksCollection;
                _supplierCollection = supplierCollection;
        }

        public async Task<List<DrinkItem>> GetAllDrinks()=>
            await _drinksCollection.Find(_ => true).ToListAsync();

        public async Task<DrinkItem?> GetDrinkByID(string id)=>
            await _drinksCollection.Find(drink => drink.DrinkItemId == id).FirstOrDefaultAsync();

        public async Task CreateDrink(DrinkItem newDrink)
        {
            if (!string.IsNullOrEmpty(newDrink.SupplierId))
            {
                var supplier = await _supplierCollection
                    .Find(s => s.SupplierId == newDrink.SupplierId)
                    .FirstOrDefaultAsync();

                if (supplier == null)
                {
                    throw new ArgumentException("Invalid Supplier Id");
                }
            }
            await _drinksCollection.InsertOneAsync(newDrink);
        }

        public async Task UpdateDrink(string id, DrinkItem updateDrink)
        {
            if (!string.IsNullOrEmpty(updateDrink.SupplierId))
            {
                var supplier = await _supplierCollection
                    .Find(s => s.SupplierId == updateDrink.SupplierId)
                    .FirstOrDefaultAsync();

                if (supplier == null)
                {
                    throw new ArgumentException("Invalid Supplier Id");
                }
            }
            await _drinksCollection.ReplaceOneAsync(drink => drink.DrinkItemId == id, updateDrink);
        }

        public async Task DeleteDrink(string id) =>
            await _drinksCollection.DeleteOneAsync(drink => drink.DrinkItemId == id);

        // One to many relationship - for drinks and supplier collections 
        // checking there is a supplier attached to the drink item
        public async Task<(DrinkItem? Drink, Supplier? Supplier)> GetDrinkRead(string id)
        {
            var drink = await _drinksCollection.Find(d => d.DrinkItemId == id).FirstOrDefaultAsync();

            if(drink == null)
            {
                return (null, null);
            }

            Supplier? supplier = null;

            if (!string.IsNullOrEmpty(drink.SupplierId))
            {
                supplier = await _supplierCollection.Find(s => s.SupplierId == drink.SupplierId).FirstOrDefaultAsync();
            }
            return (drink, supplier);
        }
    }
}
