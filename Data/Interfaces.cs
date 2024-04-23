using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullstack_portfolio.Data;

public interface IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; }

    // make sure that all models have a new instance method
    public T New<T>() where T : IMongoModel, new()
    {
        return new T();
    }
}
