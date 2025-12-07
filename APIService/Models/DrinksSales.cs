using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace APIService.Models
{
    public class DrinksSales
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DrinksSalesId { get; set; }

        public string? DrinkItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfSale {  get; set; }
    }
}
