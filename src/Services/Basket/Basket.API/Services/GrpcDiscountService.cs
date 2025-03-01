using Discount.Grpc;

namespace Basket.API.Services;

public class GrpcDiscountService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

    public GrpcDiscountService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
        _discountProtoService = discountProtoService;
    }
} 