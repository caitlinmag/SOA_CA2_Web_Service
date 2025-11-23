using APIService.Models;

namespace APIService.DTOs
{
    public class DrinkDTO
    {
        public int DrinkItemId { get; set; }
        public string? DrinkName { get; set; }
        public string? DrinkType { get; set; }
        public int Price { get; set; }
        public string? Extras { get; set; }

        public string SupplierName { get; set; }
    }
}
