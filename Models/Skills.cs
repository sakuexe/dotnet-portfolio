using System.ComponentModel.DataAnnotations;
using fullstack_portfolio.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullstack_portfolio.Models;

public enum SkillType
{
    [BsonElement("pie")] // lowercase
    [Display(Name = "Bar Chart")]
    Bar,
    [BsonElement("pie")] // lowercase
    [Display(Name = "Pie Chart")]
    Pie,
}

public class Skill : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
    [Required]
    [BsonElement("type")]
    [Display(Name = "Chart Type")]
    [DataType(DataType.Text, ErrorMessage = "Select a valid skill type. (Bar, Pie)")]
    [EnumDataType(typeof(SkillType))]
    public SkillType Type { get; set; } = SkillType.Bar;
    [Required]
    [MaxLength(64)]
    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [Range(0, 100)]
    [BsonElement("value")]
    public int Value { get; set; }

    public Skill()
    {
    }
}
