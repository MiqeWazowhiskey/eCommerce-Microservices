using Basket.API.Exceptions;

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        //var basket = await session.LoadAsync<ShoppingCart>(userId.ToString(), cancellationToken);
        var basket = await session.Query<ShoppingCart>().FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        return basket is null ? throw new BasketNotFoundException(userId) : basket;
    }

    public async Task<ShoppingCart> AddBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasketAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var basket = session.Query<ShoppingCart>().FirstOrDefault(x => x.UserId == userId);
        if (basket is null)
        {
            throw new BasketNotFoundException(userId);
        }
        session.Delete<ShoppingCart>(basket);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}