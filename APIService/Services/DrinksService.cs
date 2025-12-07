using APIService.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using APIService.Models;
using System.Formats.Asn1;
using Microsoft.AspNetCore.Mvc;

namespace APIService.Data
{
    public class DrinksService
    {
        private readonly IMongoCollection<DrinkItem> _drinksCollection;
    
        public DrinksService(IMongoCollection<DrinkItem> drinksCollection) 
        {
                _drinksCollection = drinksCollection;
        }

        public async Task<List<DrinkItem>> GetAllDrinks()=>
            await _drinksCollection.Find(_ => true).ToListAsync();

        public async Task<DrinkItem?> GetDrinkByID(string id)=>
            await _drinksCollection.Find(drink => drink.DrinkItemId == id).FirstOrDefaultAsync();

        public async Task CreateDrink(DrinkItem newDrink) =>
            await _drinksCollection.InsertOneAsync(newDrink);

        public async Task UpdateDrink(string id, DrinkItem updateDrink) =>
            await _drinksCollection.ReplaceOneAsync(drink => drink.DrinkItemId == id, updateDrink);

        public async Task DeleteDrink(string id) =>
            await _drinksCollection.DeleteOneAsync(drink => drink.DrinkItemId == id);
    }
}
