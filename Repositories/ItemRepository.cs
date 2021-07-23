using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Play.Catalog.Service.Models;

namespace Play.Catalog.Service.Repositories
{
    public class ItemRepository
    {
        private const string CollectionName = "Items";
        private const string DBName = "Catalog";
        private readonly IMongoCollection<Item> DBCollection;
        private readonly FilterDefinitionBuilder<Item> FilterDefinition = Builders<Item>.Filter;

        public ItemRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase(DBName);
            DBCollection = db.GetCollection<Item>(CollectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetItemsAsync()
        {
            return await DBCollection.Find(FilterDefinition<Item>.Empty).ToListAsync();
        }
    }
}