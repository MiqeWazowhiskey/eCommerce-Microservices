using Basket.API.Data;

namespace Basket.API.Basket.GetBasket;

public record GetBasketResult(ShoppingCart Cart);
public record GetBasketQuery(Guid UserId) : IQuery<GetBasketResult>;

public class GetBasketQueryHandler(IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasketAsync(query.UserId, cancellationToken);
        return new GetBasketResult(cart);
    }
}