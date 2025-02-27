namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasketAsync(Guid userId, CancellationToken cancellationToken= default);
    Task<ShoppingCart> AddBasketAsync(ShoppingCart basket, CancellationToken cancellationToken= default);
    Task<bool> DeleteBasketAsync(Guid userId, CancellationToken cancellationToken= default);
}