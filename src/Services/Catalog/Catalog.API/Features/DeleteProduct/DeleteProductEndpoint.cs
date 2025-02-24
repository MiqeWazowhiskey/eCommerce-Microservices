namespace Catalog.API.Features.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);

public record DeleteProductRequest(Guid Id);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{productId}",
                async (Guid productId, ISender send) =>
                {
                    var command = new DeleteProductCommand(productId);

                    var result = await send.Send(command);
                    var response = result.Adapt<DeleteProductResponse>();

                    return Results.Ok(response);
                })
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Product deleted successfully");
    }
}
