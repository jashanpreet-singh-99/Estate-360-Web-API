using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

using EState_360.Core.Entities;
using MongoDB.Bson.Serialization.Conventions;

namespace EState_360.Infrastructure.Data
{
	public class MongoDbContext
	{
		private readonly IMongoDatabase _database;
        private readonly string _collectionName;

		public MongoDbContext(IConfiguration configuration)
		{
            var client = new MongoClient(configuration["CosmosDb:ConnectionString"]);
            _database = client.GetDatabase(configuration["CosmosDb:DatabaseName"]);

            // Configure convention pack to use camelCase for property names
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, type => true);

            _collectionName = configuration["CosmosDb:CollectionName"]!;
        }

        public IMongoCollection<Listing> Listings => _database.GetCollection<Listing>(_collectionName);
    }
}