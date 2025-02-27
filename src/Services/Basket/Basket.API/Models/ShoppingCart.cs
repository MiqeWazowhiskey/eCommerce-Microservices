using Marten.Schema;

namespace Basket.API.Models;

public class ShoppingCart
{
    
    // todo from session after auth services userId
    [Identity]
    public Guid UserId { get; set; } = Guid.Empty;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Price);
    
    public ShoppingCart(Guid userId)
    {
        UserId = userId;
    }
    
    //required to map
    public ShoppingCart()
    {
    }
}