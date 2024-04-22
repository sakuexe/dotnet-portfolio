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
    [Required]
    [MaxLength(50)]
    [DataType(DataType.Text)]
    [BsonElement("title")]
    public string Title { get; set; }
    [Required]
    [MaxLength(255)]
    [DataType(DataType.MultilineText)]
    [BsonElement("description")]
    public string Description { get; set; }
    [MaxLength(50)]
    [BsonElement("icon")]
    [DataType(DataType.ImageUrl)]
    public string? Icon { get; set; }

    public Expertise()
    {
        this.Title = String.Empty;
        this.Description = String.Empty;
        this.Icon = String.Empty;
    }
}
