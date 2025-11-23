namespace APIService.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Location { get; set; }
        public int StockLevel { get; set; }

        public ICollection<DrinkItem> DrinkItems { get; set; }
    }
}
