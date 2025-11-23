namespace APIService.DTOs
{
    public class DrinkCreateDTO
    {
        public string? DrinkName { get; set; }
        public string? DrinkType { get; set; }
        public int Price { get; set; }
        public string? Extras { get; set; }

        public int SupplierId { get; set; }
    }
}
