namespace APIService.Models
{
    public class DrinksSales
    {
        public int DrinksSalesId { get; set; }

        public int DrinkItemId { get; set; }
        public DrinkItem DrinkItem { get; set; }

        public int Quantity { get; set; }
        public DateTime DateOfSale {  get; set; }
    }
}
