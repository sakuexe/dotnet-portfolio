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

    public override string ToString()
    {
        return $"{Name} - {Role}";
    }
}

public class Project : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
    [Required]
    [MaxLength(64)]
    [BsonElement("title")]
    [DataType(DataType.Text)]
    public string Title { get; set; }
    [Required]
    [BsonElement("category")]
    [EnumDataType(typeof(CategoryType))]
    public CategoryType Category { get; set; }
    [MaxLength(5012)]
    [BsonElement("description")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [BsonElement("finishedAt")]
    public DateOnly? FinishedAt { get; set; }

    [BsonElement("tags")]
    public List<string> Tags { get; set; }
    [BsonElement("team")]
    public List<TeamMember> Team { get; set; }

    [BsonElement("imageUrl")]
    [DataType(DataType.ImageUrl)]
    public string? ImageUrl { get; set; }
    [DataType(DataType.Text)]
    [BsonElement("altText")]
    public string? AltText { get; set; }
    [BsonElement("isLarge")]
    public bool IsLarge { get; set; }

    public Project()
    {
        Title = string.Empty;
        Category = CategoryType.Other;
        Tags = new List<string>();
        Team = new List<TeamMember>();
    }
}
