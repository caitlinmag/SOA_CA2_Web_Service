namespace APIService.DTOs
{
    public class DrinkReadDTO
    {
        // Drink and Supplier fields included
        public string? DrinkItemId { get; set; }
        public string? DrinkName { get; set; }
        public string? DrinkType { get; set; }
        public double Price { get; set; }
        public string? Extras { get; set; }

        // one to many relationship with supplier collection
        public string? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Location { get; set;}
    }
}
