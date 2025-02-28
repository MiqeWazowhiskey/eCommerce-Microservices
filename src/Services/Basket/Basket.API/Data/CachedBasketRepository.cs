using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
    :IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userId.ToString(),cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
           return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }
        
        var basket = await repository.GetBasketAsync(userId, cancellationToken);
        await cache.SetStringAsync(userId.ToString(),JsonSerializer.Serialize(basket),cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> AddBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await repository.AddBasketAsync(basket, cancellationToken);
        await cache.SetStringAsync(basket.UserId.ToString(),JsonSerializer.Serialize(basket),cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasketAsync(Guid userId, CancellationToken cancellationToken = default)
    {
       await repository.DeleteBasketAsync(userId, cancellationToken);
       await cache.RemoveAsync(userId.ToString(),cancellationToken); 
       return true;
    }
}