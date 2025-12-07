namespace APIService.DTOs
{
    public class SaleDTO
    {
        public string? DrinksSalesId { get; set; }
        public string? DrinkItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfSale { get; set; }
    }
}
