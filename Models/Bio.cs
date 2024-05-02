using System.ComponentModel.DataAnnotations;
using fullstack_portfolio.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullstack_portfolio.Models;

public class Bio : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
    [Display(Name = "Updated At")]
    [HiddenInput]
    public DateTime UpdatedAt { get; set; }
    [Required]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public Bio()
    {
        UpdatedAt = DateTime.Now;
        Description = string.Empty;
        Email = string.Empty;
    }
}
