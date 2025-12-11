using APIService.Interfaces;
using APIService.Models;
using MongoDB.Driver;

namespace APIService.Services
{
    public class DrinkSaleService : IDrinkSalesService
    {

        private readonly IMongoCollection<DrinksSales> _salesCollection;
        private readonly IMongoCollection<DrinkItem> _drinksCollection;


        public DrinkSaleService(IMongoCollection<DrinksSales> salesCollection, IMongoCollection<DrinkItem> drinksCollection)
        {
            _salesCollection = salesCollection;
            _drinksCollection = drinksCollection;
        }

        public async Task<List<DrinksSales>> GetAllSales() =>
            await _salesCollection.Find(_ => true).ToListAsync();

        public async Task<DrinksSales?> GetSaleById(string id) =>
            await _salesCollection.Find(sale => sale.DrinksSalesId == id).FirstOrDefaultAsync();

        public async Task CreateSale(DrinksSales newSale)
        {
            if (!string.IsNullOrEmpty(newSale.DrinkItemId))
            {
                var drink = await _drinksCollection
                    .Find(d => d.DrinkItemId  == newSale.DrinkItemId)
                    .FirstOrDefaultAsync();

                if (drink == null)
                {
                    throw new ArgumentException("Invalid Drink Id");
                }
            }
            await _salesCollection.InsertOneAsync(newSale);
        }

        public async Task UpdateSale(string id, DrinksSales updateDrink) =>
            await _salesCollection.ReplaceOneAsync(sale => sale.DrinksSalesId == id, updateDrink);

        public async Task DeleteSale(string id) =>
            await _salesCollection.DeleteOneAsync(sale => sale.DrinksSalesId == id);

        // One to many relationship - for drinks and supplier collections 
        public async Task<(DrinksSales? Sale, DrinkItem? Drink)> GetSaleRead(string id)
        {
            // checking there is a sale attached to the drink item
            var sale = await _salesCollection.Find(s => s.DrinksSalesId == id).FirstOrDefaultAsync();

            if (sale == null)
            {
                return (null, null);
            }

            DrinkItem? drink = null;

            if (!string.IsNullOrEmpty(sale.DrinkItemId))
            {
                drink = await _drinksCollection.Find(d => d.DrinkItemId == sale.DrinkItemId).FirstOrDefaultAsync();
            }
            return (sale, drink);
        }
    }
}
