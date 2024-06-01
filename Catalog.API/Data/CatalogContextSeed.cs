using Catalog.API.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Catalog.API.Data
{
    public class CatalogContextSeed : ICatalogContextSeed
    {
        public async Task SeedDataAsync(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(x => true).Any();
            if (!existProduct)
                await productCollection.InsertManyAsync(GetProducts());
        }

        public static IEnumerable<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Smartphone Alpha",
                    Category = "Celulares",
                    Description = "Smartphone Alpha com tela de 6.1 polegadas, 128GB de armazenamento e câmera de 12MP.",
                    Image = "https://example.com/images/smartphone_alpha.jpg",
                    Price = 3999.99M
                },
                new Product()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Smartphone Beta",
                    Category = "Celulares",
                    Description = "Smartphone Beta com tela de 6.5 polegadas, 256GB de armazenamento e câmera de 48MP.",
                    Image = "https://example.com/images/smartphone_beta.jpg",
                    Price = 4999.99M
                },
                new Product()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Smartphone Gamma",
                    Category = "Celulares",
                    Description = "Smartphone Gamma com tela de 6.7 polegadas, 512GB de armazenamento e câmera de 108MP.",
                    Image = "https://example.com/images/smartphone_gamma.jpg",
                    Price = 5999.99M
                },
                new Product()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Smartphone Delta",
                    Category = "Celulares",
                    Description = "Smartphone Delta com tela de 5.8 polegadas, 64GB de armazenamento e câmera de 12MP.",
                    Image = "https://example.com/images/smartphone_delta.jpg",
                    Price = 2999.99M
                },
                new Product()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Smartphone Epsilon",
                    Category = "Celulares",
                    Description = "Smartphone Epsilon com tela de 6.3 polegadas, 128GB de armazenamento e câmera de 24MP.",
                    Image = "https://example.com/images/smartphone_epsilon.jpg",
                    Price = 3999.99M
                }
            };
        }
    }
}
