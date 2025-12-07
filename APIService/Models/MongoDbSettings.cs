namespace APIService.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string DrinksCollectionName { get; set; } = "Drinks";
        public string SalesCollectionName { get; set; } = "DrinkSales";
        public string SuppliersCollectionName { get; set; } = "Suppliers";

    }
}
