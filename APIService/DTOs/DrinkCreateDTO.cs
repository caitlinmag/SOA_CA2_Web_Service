namespace APIService.DTOs
{
    public class DrinkCreateDTO
    {
        public string? DrinkItemId { get; set; }
        public string? DrinkName { get; set; }
        public string? DrinkType { get; set; }
        public int Price { get; set; }
        public string? Extras { get; set; }

        public string? SupplierId { get; set; }
    }
}
