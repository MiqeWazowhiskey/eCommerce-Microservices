using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data;

public class DiscountContext : DbContext
{
    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = Guid.Parse("3c05ba13-45d5-4c8b-b979-5a688ad87693"), ProductId = Guid.Parse("0195431f-1640-4bdf-8158-be6237c91ce4"), Description = "Test Coupon", Amount = 10 },
            new Coupon { Id = Guid.Parse("9455f1dd-2cf2-4a8d-b271-52f8d2f07c41"), ProductId = Guid.Parse("0195431f-aa38-43d7-8991-711331f33d3a"), Description = "Test Coupon2", Amount = 20 }
            );

    }
    
}