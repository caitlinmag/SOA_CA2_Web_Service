using APIService.Models;
using MongoDB.Driver;

namespace APIService.Services
{
    public class DrinkSaleService
    {
        private readonly IMongoCollection<DrinksSales> _salesCollection;

        public DrinkSaleService(IMongoCollection<DrinksSales> salesCollection)
        {
            _salesCollection = salesCollection;
        }

        public async Task<List<DrinksSales>> GetAllSales() =>
            await _salesCollection.Find(_ => true).ToListAsync();

        public async Task<DrinksSales?> GetSaleById(string id) =>
            await _salesCollection.Find(sale => sale.DrinksSalesId == id).FirstOrDefaultAsync();

        public async Task CreateSale(DrinksSales newSale) =>
            await _salesCollection.InsertOneAsync(newSale);

        public async Task UpdateSale(string id, DrinksSales updateDrink) =>
            await _salesCollection.ReplaceOneAsync(sale => sale.DrinksSalesId == id, updateDrink);

        public async Task DeleteSale(string id) =>
            await _salesCollection.DeleteOneAsync(sale => sale.DrinksSalesId == id);
    }
}
