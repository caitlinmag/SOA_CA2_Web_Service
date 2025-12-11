using APIService.Models;

namespace APIService.Interfaces
{
    public interface IDrinksService
    {
        Task<List<DrinkItem>> GetAllDrinks();
        Task<DrinkItem?> GetDrinkByID(string id);
        Task CreateDrink(DrinkItem newDrink);
        Task UpdateDrink(string id, DrinkItem updateDrink);
        Task DeleteDrink(string id);
        Task<(DrinkItem? Drink, Supplier? Supplier)> GetDrinkRead(string id);
    }
}
