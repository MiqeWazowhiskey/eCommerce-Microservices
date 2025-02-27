namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userID}",
                async (ISender sender, Guid userId) =>
                {
                    var command = new DeleteBasketCommand(userId);
                    var result = await sender.Send(command);
                    var response = result.Adapt<DeleteBasketResponse>();

                    return Results.Ok(response);
                })
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("DeleteBasket")
            .WithDescription("Delete the basket for a user")
            .WithSummary("Delete the basket for a user");
    }
}