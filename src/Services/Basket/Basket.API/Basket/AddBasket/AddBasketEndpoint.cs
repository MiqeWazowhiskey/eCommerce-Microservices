namespace Basket.API.Basket.AddBasket;

public record AddBasketResponse(bool IsSuccess, Guid UserId);
public record AddBasketRequest(ShoppingCart Cart);
public class AddBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost<AddBasketCommand>("/basket",
                async (AddBasketRequest request, ISender sender) =>
                {
                    var command = request.Adapt<AddBasketCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<AddBasketResponse>();

                    return Results.Created($"/basket/{response.UserId}", response);
                })
            .Produces<AddBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}