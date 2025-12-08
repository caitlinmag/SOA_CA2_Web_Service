namespace APIService.DTOs
{
    public class DrinkUpdateDTO
    {
        // not allowing to change the ID 
        public string? DrinkName { get; set; }
        public string? DrinkType { get; set; }
        public double Price { get; set; }
        public string? Extras { get; set; }
        public string? SupplierId { get; set; }
    }
}
