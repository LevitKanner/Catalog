using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Play.Catalog.Service.Models;

namespace Play.Catalog.Service.Repositories
{
    public class ItemRepository : IItemRepository
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

        public async Task<Item> GetItemAsync (Guid id)
        {
            var filter = FilterDefinition.Eq(item => item.Id, id);
            return await DBCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateItemAsync(Item item)
        {
            if (item == null) throw new ArgumentException(nameof(item)); 
            await DBCollection.InsertOneAsync(item);
        }

        public async Task UpdateItem(Item item)
        {
            if (item == null) throw new ArgumentException(null, nameof(item));
            var filter = FilterDefinition.Eq(entity => entity.Id, item.Id);
            await DBCollection.ReplaceOneAsync(filter, item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = FilterDefinition.Eq(item => item.Id, id);
            await DBCollection.DeleteOneAsync(filter);
        }
    }
}