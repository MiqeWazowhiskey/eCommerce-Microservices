namespace Catalog.API.Features.UpdateProduct;

public record UpdateProductResponse(Product Product);

public record UpdateProductRequest(
    string? Name,
    string? Description,
    decimal? Price,
    string? ImageFile,
    List<string>? Category);

public class UpdateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{productId}",
                async (Guid productId, UpdateProductRequest request, ISender send) =>
                {
                    var command = new UpdateProductCommand(
                        Id: productId,
                        Name: request.Name,
                        Description: request.Description,
                        Price: request.Price,
                        ImageFile: request.ImageFile,
                        Category: request.Category);
                    var result = await send.Send(command);
                    var response = result.Adapt<UpdateProductResponse>();

                    return Results.Ok(response);
                })
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Product updated successfully");
    }
}
