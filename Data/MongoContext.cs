using System.Diagnostics;
using fullstack_portfolio.Models;
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
    private static string GetCollectionName<T>(T? record)
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

    public static Task<T> Save<T>(T record) where T : IMongoModel
    {
        // use the classname of the passed object as the table name
        // fullstack_portfolio.Models.User -> user
        var table = GetCollectionName<T>(default);
        try
        {
            var mongoCollection = Database?.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", record._id);
            // use the upsert option. If the record is not found, it will be inserted
            var usingUpsert = new ReplaceOptions { IsUpsert = true };
            // upsert the record (update-insert)
            mongoCollection?.ReplaceOneAsync(filter, record, usingUpsert);
        }
        catch (Exception e)
        {
            Console.WriteLine("Skill Issue - Saving record failed");
            Console.WriteLine(e.Message);
        }
        return Task.FromResult(record);
    }

    public static Task SaveMultiple<T>(List<T> records) where T : IMongoModel
    {
        var table = GetCollectionName<T>(default);
        try
        {
            var mongoCollection = Database?.GetCollection<T>(table);
            mongoCollection?.InsertManyAsync(records);
        }
        catch (Exception e)
        {
            Console.WriteLine("Skill Issue - Saving multiple records failed");
            Console.WriteLine(e.Message);
        }
        return Task.CompletedTask;
    }

    public static T? Get<T>(string? id) where T : IMongoModel
    {
        var table = GetCollectionName<T>(default);
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

    public static T? GetLatest<T>() where T : IMongoModel
    {
        var table = GetCollectionName<T>(default);
        try
        {
            var mongoCollection = Database?.GetCollection<T>(table);
            var results = mongoCollection
                .AsQueryable()
                .OrderByDescending(c => c._id)
                .FirstOrDefault();
            return results;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return default;
        }
    }

    public async static Task<List<T>> GetAll<T>(string? collectionName = null) where T : IMongoModel
    {
        var table = GetCollectionName<T>(default);
        if (collectionName != null)
            table = collectionName;
        try
        {
            var mongoCollection = Database?.GetCollection<T>(table);
            var resultsList = await mongoCollection.FindAsync(new BsonDocument());
            return resultsList.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Skill Issue - Getting all records failed");
            Console.WriteLine(e.Message);
            return new List<T>();
        }
    }

    public static async Task<List<T>> Filter<T>(string column, dynamic value) where T : IMongoModel
    {
        var table = GetCollectionName<T>(default);
        try
        {
            var mongoCollection = Database?.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq(column, value);
            return await mongoCollection?.FindAsync(filter);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return new List<T>();
        }
    }

    public static List<string> GetCollectionNames()
    {
        var collections = Database?.ListCollectionNames().ToList();
        // reverse the order, so that it will be in the order of appearance
        // in the database
        collections?.Reverse();
        return collections ?? new List<string>();
    }
}
