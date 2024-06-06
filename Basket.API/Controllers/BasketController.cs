using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IDiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetBasketAsync(userName);

            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPut]
        public async Task<ActionResult<ShoppingCart>> SetBasket(ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                if (String.IsNullOrEmpty(item.ProductName))
                    return BadRequest($"Basket item id {item.ProductId} does not have a name.");

                try
                {
                    var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                    item.Price -= coupon.Amount;
                }
                catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"An error has occurred.\nDetails: {ex.Message}");
                }
                
            }

            return Ok(await _basketRepository.SetBasketAsync(basket)); 
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasketAsync(userName);
            return Ok();
        }
    }

}
