using System;
using ReloadingClasses;
using MongoDB.Bson;

namespace LoadDataAPI.Models
{
    public class MongoCustomLoad : CustomLoad
    {
        public ObjectId Id { get; set; }

        public MongoCustomLoad(ObjectId Id) : base()
        {
            this.Id = Id;
        }
    }
}

