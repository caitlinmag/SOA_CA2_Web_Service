using APIService.Models;

namespace APIService.Interfaces
{
    public interface IDrinkSalesService
    {
        Task<List<DrinksSales>> GetAllSales();
        Task<DrinksSales?> GetSaleById(string id);
        Task CreateSale(DrinksSales newsSales);
        Task UpdateSale(string id, DrinksSales updateDrink);
        Task DeleteSale(string id);
        Task<(DrinksSales? Sale, DrinkItem? Drink)> GetSaleRead(string id);
    }
}
