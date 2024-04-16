using fullstack_portfolio.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullstack_portfolio.Models;

public class Expertise : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
    public string title { get; set; }
    public string description { get; set; }
    public string icon { get; set; }

    public Expertise()
    {
        this.title = String.Empty;
        this.description = String.Empty;
        this.icon = String.Empty;
    }
}
