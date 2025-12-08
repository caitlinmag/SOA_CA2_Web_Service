using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace APIService.Models
{
    public class DrinkItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DrinkItemId { get; set; }
        public string? DrinkName { get; set; }

        public string? DrinkType { get; set; }

        public double Price { get; set; }

        public string? Extras { get; set; }

        public string? SupplierId { get; set; }

    }
}
