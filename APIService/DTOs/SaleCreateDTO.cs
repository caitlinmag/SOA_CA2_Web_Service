namespace APIService.DTOs
{
    public class SaleCreateDTO
    {
        public int DrinkItemId { get; set; }
        public string DrinkName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfSale { get; set; }
    }
}
