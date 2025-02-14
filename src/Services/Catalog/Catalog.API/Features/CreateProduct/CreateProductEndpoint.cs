namespace Catalog.API.Features.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    List<string> Category);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
                async (CreateProductRequest request, ISender send) =>
                {
                    var command = request.Adapt<CreateProductCommand>();

                    var result = await send.Send(command);
                    var response = result.Adapt<CreateProductResponse>();

                    return Results.Created($"/products/{response.Id}", response);
                })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Product created successfully");
    }
}
