using LoadDataAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace LoadDataAPI.Services
{
    public class FactoryLoadService
    {
        private readonly IMongoCollection<MongoFactoryLoad> _factoryLoadsCollection;

        public FactoryLoadService(IOptions<FactoryLoadSettings> factoryLoadSettings)
        {
            MongoClient mongoClient = new MongoClient(factoryLoadSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(factoryLoadSettings.Value.DatabaseName);

            _factoryLoadsCollection = mongoDatabase.GetCollection<MongoFactoryLoad>(factoryLoadSettings.Value.FactoryCollectionName);
        }

        public async Task<List<MongoFactoryLoad>> GetAsync() =>
            await _factoryLoadsCollection.Find(_ => true).ToListAsync();


        public async Task<MongoFactoryLoad?> GetAsync(ObjectId id) =>
            await _factoryLoadsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(MongoFactoryLoad newFactoryLoad) =>
            await _factoryLoadsCollection.InsertOneAsync(newFactoryLoad);

        public async Task UpdateAsync(ObjectId id, MongoFactoryLoad updatedFactoryLoad) =>
            await _factoryLoadsCollection.ReplaceOneAsync(x => x.Id == id, updatedFactoryLoad);

        public async Task RemoveAsync(ObjectId id) =>
            await _factoryLoadsCollection.DeleteOneAsync(x => x.Id == id);
    }
}

