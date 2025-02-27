namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = string.Empty;
    //User object must be here after authentication services
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Price);
    
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
    
    //required to map
    public ShoppingCart()
    {
    }
}