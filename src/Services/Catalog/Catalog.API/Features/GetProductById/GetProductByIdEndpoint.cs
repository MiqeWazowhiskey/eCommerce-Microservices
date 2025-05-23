namespace Catalog.API.Features.GetProductById;

public record GetProductByIdResponse(Product Product);



public class GetProductByIdEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{productId}",
                async (ISender send, Guid productId) =>
                {
                    var query = new GetProductByIdQuery(productId);
                    var result = await send.Send(query);
                    var response = result.Adapt<GetProductByIdResponse>();

                    return Results.Ok(response);
                })
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Product by Id")
            .WithDescription("Get a product by its id");
    }
}
