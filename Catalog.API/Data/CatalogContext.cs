using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration configuration, ICatalogContextSeed catalogContextSeed)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            InitializeAsync(catalogContextSeed).GetAwaiter().GetResult();
        }

        private async Task InitializeAsync(ICatalogContextSeed catalogContextSeed)
        {
            await catalogContextSeed.SeedDataAsync(Products);
        }
    }
}
