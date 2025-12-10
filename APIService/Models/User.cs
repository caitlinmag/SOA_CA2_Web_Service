using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace APIService.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 
        [BsonElement("UserName")]
        public string? UserName {  get; set; } = string.Empty;
        [BsonElement("Password")]
        public string? Password { get; set; } = string.Empty;
    }
}
