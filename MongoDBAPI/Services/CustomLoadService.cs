﻿using LoadDataAPI.Models;
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

        public async Task<List<MongoCustomLoad>> GetAsync() =>
            await _customLoadsCollection.Find(_ => true).ToListAsync();


        public async Task<MongoCustomLoad?> GetAsync(ObjectId id) =>
            await _customLoadsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(MongoCustomLoad newCustomLoad) =>
            await _customLoadsCollection.InsertOneAsync(newCustomLoad);

        public async Task UpdateAsync(ObjectId id, MongoCustomLoad updatedCustomLoad) =>
            await _customLoadsCollection.ReplaceOneAsync(x => x.Id == id, updatedCustomLoad);

        public async Task RemoveAsync(ObjectId id) =>
            await _customLoadsCollection.DeleteOneAsync(x => x.Id == id);
    }
}

