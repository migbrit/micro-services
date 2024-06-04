using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart?> GetBasketAsync(string userName);
        Task<ShoppingCart?> SetBasketAsync(ShoppingCart basket);
        Task DeleteBasketAsync(string userName);
    }
}
