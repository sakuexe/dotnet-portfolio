using System.ComponentModel.DataAnnotations;
using fullstack_portfolio.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullstack_portfolio.Models;

public class ContactInfo : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public List<string> Links { get; set; }

    public ContactInfo()
    {
        Email = string.Empty;
        Links = new List<string>();
    }
}
