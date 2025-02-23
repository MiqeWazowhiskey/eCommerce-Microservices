namespace Catalog.API.Features.GetProductByCategory;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);
public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Query}", query);

        var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync();
        if (products is null)
        {
            throw new ProductNotFoundException();
        }
        
        return new GetProductsByCategoryResult(products);
    }
}
