namespace Discount.gRPC.Models;

public class Coupon
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
}