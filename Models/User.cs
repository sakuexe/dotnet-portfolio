using System.ComponentModel.DataAnnotations;
using fullstack_portfolio.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullstack_portfolio.Models;

public class User : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
    [Required]
    [DataType(DataType.Text)]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }

    public User() { }
}
