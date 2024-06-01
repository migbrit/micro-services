using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public interface ICatalogContextSeed
    {
        Task SeedDataAsync(IMongoCollection<Product> productCollection);
    }
}
