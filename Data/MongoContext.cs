using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fullstack_portfolio.Data;

public class MongoContext
{
    private static string? DATABASE_NAME => Config?.GetConnectionString("DatabaseName");
    private static string? HOST => Config?.GetConnectionString("DefaultConnection");
    // the configuration
    private static IConfiguration? Config { get; set; }
    private static MongoServerAddress? ServerAddress { get; set; }
    private static MongoClientSettings? Settings { get; set; }
    private static MongoClient? Client { get; set; }
    private static IMongoDatabase? Database { get; set; }

    // make a method for the naming convention, all other methods
    // get updated at the same time
    private static string GetCollectionName<T>(T record)
    {
        return typeof(T).Name.ToLower();
    }

    public static void Initialize(IConfiguration config)
    {
        Config = config;
        ServerAddress = new MongoServerAddress(HOST);
        Settings = new MongoClientSettings
        {
            Server = ServerAddress
        };
        Client = new MongoClient(Settings);
        Database = Client.GetDatabase(DATABASE_NAME);
    }

    public static T Save<T>(T record) where T : IMongoModel
    {
        // use the classname of the passed object as the table name
        // fullstack_portfolio.Models.User -> user
        var table = GetCollectionName<T>(record);
        try
        {
            var mongoCollection = Database?.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", record._id);
            // use the upsert option. If the record is not found, it will be inserted
            var usingUpsert = new ReplaceOptions { IsUpsert = true };
            // upsert the record (update-insert)
            mongoCollection?.ReplaceOne(filter, record, usingUpsert);
        }
        catch (Exception e)
        {
            Console.WriteLine("Skill Issue");
            Console.WriteLine(e.Message);
        }
        return record;
    }

    public static T? Get<T>(string? id) where T : IMongoModel
    {
        var table = typeof(T).Name.ToLower();
        try
        {
            var mongoCollection = Database?.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return mongoCollection.Find(filter).FirstOrDefault();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return default;
        }
    }

    public static List<T> GetAll<T>(string? collectionName) where T : IMongoModel
    {
        var table = typeof(T).Name.ToLower();
        try
        {
            var mongoCollection = Database?.GetCollection<T>(table);
            var resultsList = mongoCollection!.Find(new BsonDocument()).ToList();
            if (!mongoCollection.AsQueryable().Any())
            {
                Console.WriteLine("No results found");
            }
            return mongoCollection?.Find(new BsonDocument()).ToList() ?? new List<T>();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return new List<T>();
        }
    }

    public static List<string> GetCollectionNames()
    {
        return Database?.ListCollectionNames().ToList() ?? new List<string>();
    }
}
