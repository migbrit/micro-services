using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDiscountAsync(productName);
            if (coupon == null) return NotFound();

            return Ok(coupon);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDiscount(Coupon coupon)
        {
            var created = await _discountRepository.CreateDiscountAsync(coupon);
            if(!created)
                return BadRequest();

            return CreatedAtAction(nameof(GetDiscount), new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDiscount(Coupon coupon)
        {
            var updated = await _discountRepository.UpdateDiscountAsync(coupon);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteDiscount")]
        public async Task<ActionResult> DeleteDiscount(string id)
        {
            var deleted = await _discountRepository.DeleteDiscountAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
