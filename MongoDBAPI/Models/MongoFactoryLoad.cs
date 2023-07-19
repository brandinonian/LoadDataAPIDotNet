using System;
using ReloadingClasses;
using MongoDB.Bson;

namespace LoadDataAPI.Models
{
    public class MongoFactoryLoad : FactoryLoad
    {
        public ObjectId Id { get; set; }

        public MongoFactoryLoad(ObjectId Id) : base()
        {
            this.Id = Id;
        }
    }
}

