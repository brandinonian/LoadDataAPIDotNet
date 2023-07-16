using LoadDataAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace LoadDataAPI.Services
{
	public class CustomLoadService 
	{
		private readonly IMongoCollection<MongoCustomLoad> _customLoadsCollection;

        public CustomLoadService(IOptions<CustomLoadSettings> CustomLoadSettings)
        {
            MongoClient mongoClient = new MongoClient(CustomLoadSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(CustomLoadSettings.Value.DatabaseName);

            _customLoadsCollection = mongoDatabase.GetCollection<MongoCustomLoad>(CustomLoadSettings.Value.CustomCollectionName);
        }

        public async Task<List<MongoCustomLoad>> GetAsync()
        {
            return await _customLoadsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<MongoCustomLoad?> GetAsync(ObjectId id)
        {
            return await _customLoadsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(MongoCustomLoad newCustomLoad)
        {
            await _customLoadsCollection.InsertOneAsync(newCustomLoad);
            return;
        }

        public async Task UpdateAsync(ObjectId id, MongoCustomLoad updatedCustomLoad) =>
            await _customLoadsCollection.ReplaceOneAsync(x => x.Id == id, updatedCustomLoad);

        public async Task RemoveAsync(ObjectId id)
        {
            FilterDefinition<MongoCustomLoad> filter = Builders<MongoCustomLoad>.Filter.Eq("Id", id);
            await _customLoadsCollection.DeleteOneAsync(filter);
            return;
        }
    }
}

