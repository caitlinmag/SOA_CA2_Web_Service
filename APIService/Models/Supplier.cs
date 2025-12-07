using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace APIService.Models
{
    public class Supplier
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Location { get; set; }
        public int StockLevel { get; set; }
    }
}
