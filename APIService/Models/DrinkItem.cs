namespace APIService.Models
{
    public class DrinkItem
    {
        public int DrinkItemId { get; set; }
        public string? DrinkName { get; set; }
        public string? DrinkType { get; set; }
        public decimal Price { get; set; }
        public string? Extras { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<DrinksSales> Sales { get; set; }

    }
}
