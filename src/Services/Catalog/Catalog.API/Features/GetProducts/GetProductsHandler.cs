namespace Catalog.API.Features.GetProducts;
public record GetProductsResult(IEnumerable<Product> ProductsResult);
public record GetProductsQuery : IQuery<GetProductsResult>;

public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IRequestHandler<GetProductsQuery,GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}
