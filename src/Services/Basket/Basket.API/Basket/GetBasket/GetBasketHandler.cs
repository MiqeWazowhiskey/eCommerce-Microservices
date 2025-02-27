namespace Basket.API.Basket.GetBasket;

public record GetBasketResult(ShoppingCart Cart);
public record GetBasketQuery(Guid UserId) : IQuery<GetBasketResult>;

public class GetBasketHandler
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        //todo after repositories
        throw new NotImplementedException();
    }
}