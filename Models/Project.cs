using System.ComponentModel.DataAnnotations;
using fullstack_portfolio.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullstack_portfolio.Models;

public enum CategoryType
{
    [BsonElement("webDevelopment")]
    [Display(Name = "Web Development")]
    WebDevelopment,
    [BsonElement("graphicDesign")]
    [Display(Name = "Graphic Design")]
    GraphicDesign,
    Other
}

public class TeamMember
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? Link { get; set; }
}

public class Project : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
    [Required]
    [MaxLength(64)]
    [BsonElement("title")]
    public string Title { get; set; }
    [Required]
    [BsonElement("category")]
    [Display(Name = "Chart Type")]
    [EnumDataType(typeof(CategoryType))]
    public CategoryType Category { get; set; }
    [MaxLength(5012)]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateOnly? FinishedAt { get; set; }
    public List<string> Tags { get; set; }
    public List<TeamMember> Team { get; set; }
    [DataType(DataType.ImageUrl)]
    public string? ImageUrl { get; set; }
    public bool IsLarge { get; set; }

    public Project()
    {
        Category = CategoryType.Other;
        Tags = new List<string>();
        Team = new List<TeamMember>();
    }
}
