namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userID}",
                async (Guid userId, ISender sender) =>
                {
                    var query = new GetBasketQuery(userId);
                    var result = await sender.Send(query);
                    var response = result.Adapt<GetBasketResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetBasket")
            .WithDescription("Get the basket for a user")
            .WithSummary("Get the basket for a user")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}