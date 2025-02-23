namespace Catalog.API.Features.GetProductByCategory;

public record GetProductsByCategoryResponse(IEnumerable<Product> Product);

public class GetProductByCategoryEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
                async (ISender send, string category) =>
                {
                    var query = new GetProductsByCategoryQuery(category);
                    var result = await send.Send(query);
                    var response = result.Adapt<GetProductsByCategoryResponse>();

                    return Results.Ok(response);
                })
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Products by Category")
            .WithDescription("Get products by category");
    }
}
