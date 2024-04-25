using System.ComponentModel.DataAnnotations;
using fullstack_portfolio.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullstack_portfolio;

public class Experience : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
    [Required]
    [DataType(DataType.Text)]
    public string Title { get; set; }
    [Required]
    [DataType(DataType.Text)]
    public string Subtitle { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateOnly StartDate { get; set; }
    [DataType(DataType.Date)]
    public DateOnly? EndDate { get; set; }
    [Required]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
    [Required]
    public List<string> Skills { get; set; }
    [DataType(DataType.ImageUrl)]
    public string? ImageUrl { get; set; }
    [DataType(DataType.Text)]
    public string? AltText { get; set; }

    public Experience()
    {
        Title = string.Empty;
        StartDate = DateOnly.FromDateTime(DateTime.UtcNow);
        Description = string.Empty;
        Skills = new List<string>();
    }
}
