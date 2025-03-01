using Discount.Grpc;

namespace Basket.API.Services;

public class GrpcDiscountService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

    public GrpcDiscountService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
        _discountProtoService = discountProtoService;
    }

    public async Task<CouponModel> GetDiscount(string productId)
    {
        var discountRequest = new GetDiscountRequest { ProductId = productId };
        return await _discountProtoService.GetDiscountAsync(discountRequest);
    }
} 