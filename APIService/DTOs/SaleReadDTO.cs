namespace APIService.DTOs
{
    public class SaleReadDTO
    {
        // sales fields
        public string? DrinksSalesId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfSale { get; set; }

        // drink item fields 
        public string? DrinkItemId { get; set; }
        public string? DrinkName { get; set; }
        public double Price { get; set; }
            
    }
}
