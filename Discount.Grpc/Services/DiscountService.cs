using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;   
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscountAsync(request.Productname);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound,
                    $"Discount for product = {request.Productname} not found."));

            _logger.LogInformation($"Discount retrieved for product {coupon.ProductName}");

            var couponModel = _mapper.Map<CouponModel>(coupon);

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            bool created = await _discountRepository.CreateDiscountAsync(coupon);
            if (!created)
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    $"An error has occurred while trying to create the product {request.Coupon.Productname}"));

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            bool updated = await _discountRepository.UpdateDiscountAsync(coupon);
            if (!updated)
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    $"An error has occurred while trying to update the product {request.Coupon.Productname}"));

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            if (!await _discountRepository.DeleteDiscountAsync(request.Productname))
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    $"An error has occurred while trying to delete the product {request.Productname}"));

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
