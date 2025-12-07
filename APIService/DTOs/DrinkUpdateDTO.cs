namespace APIService.DTOs
{
    public class DrinkUpdateDTO
    {
        public string? DrinkName { get; set; }
        public string? DrinkType { get; set; }
        public double Price { get; set; }
        public string? Extras { get; set; }
    }
}
