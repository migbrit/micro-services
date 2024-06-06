using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService : IDiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountGrpcClient;
        private readonly ILogger<DiscountGrpcService> _logger;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountGrpcClient, ILogger<DiscountGrpcService> logger)
        {
            _discountGrpcClient = discountGrpcClient;
            _logger = logger;

        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            if (string.IsNullOrEmpty(productName))
                throw new ArgumentException($"Product name {productName} cannot be empty.");

            try
            {
                var couponModel = await _discountGrpcClient.GetDiscountAsync(new GetDiscountRequest { Productname = productName });
                _logger.LogInformation($"Successfully call for gRPC service for product {productName}");
                return couponModel;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error has occurred calling gRPC service to get discount for the product {productName}.\nError details: {ex.Message}");
                throw;
            }
        }
    }
}
