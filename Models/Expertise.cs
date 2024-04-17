using System.ComponentModel.DataAnnotations;
using fullstack_portfolio.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullstack_portfolio.Models;

public class Expertise : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
    [MaxLength(50)]
    [Required]
    [DataType(DataType.Text)]
    public string title { get; set; }
    [MaxLength(255)]
    [Required]
    [DataType(DataType.MultilineText)]
    public string description { get; set; }
    [MaxLength(50)]
    public string icon { get; set; }

    public Expertise()
    {
        this.title = String.Empty;
        this.description = String.Empty;
        this.icon = String.Empty;
    }
}
