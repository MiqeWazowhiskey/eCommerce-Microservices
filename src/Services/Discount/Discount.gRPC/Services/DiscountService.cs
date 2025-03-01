using Discount.Grpc;
using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services;

public class DiscountService
    : DiscountProtoService.DiscountProtoServiceBase

{
    //mapster reinstalled in case grpc is not typical microservice
    private DiscountContext _context;
    private ILogger<DiscountService> _logger;

    public DiscountService(DiscountContext context, ILogger<DiscountService> logger)
    {
        _logger = logger;
        _context = context;
    }
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(x => string.Equals(x.ProductId,request.ProductId)) ??
                     new Coupon()
        {
            ProductId = Guid.Parse(request.ProductId),
            Amount = 0,
            Description = "No Discount"
        };

        _logger.LogInformation("Discount is retrieved for ProductId: {ProductId}, Amount: {Amount}", coupon.ProductId, coupon.Amount);
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(x => string.Equals(x.ProductId,request.Coupon.ProductId));
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductId={request.Coupon.ProductId} is not found."));
        }
        coupon.Amount = request.Coupon.Amount;
        coupon.Description = request.Coupon.Description;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Discount is successfully updated. ProductId: {ProductId}, Amount: {Amount}", coupon.ProductId, coupon.Amount);
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon()
        {
            ProductId = Guid.Parse(request.Coupon.ProductId),
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description
        };
        
        _context.Coupons.Add(coupon);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Discount is successfully created. ProductId: {ProductId}, Amount: {Amount}", coupon.ProductId, coupon.Amount);
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(x=>string.Equals(x.ProductId,request.ProductId));
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductId={request.ProductId} is not found."));
        }
        _context.Coupons.Remove(coupon);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Discount is successfully deleted. ProductId: {ProductId}", request.ProductId);
        return new DeleteDiscountResponse()
        {
            Success = true
        };
    }
}