namespace Catalog.API.Features.GetProductByCategory;

public record GetProductByCategoryResult(IEnumerable<Product> Products);
public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Query}", query);

        var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync();
        if (products is null)
        {
            throw new ProductNotFoundException();
        }
        
        return new GetProductByCategoryResult(products);
    }
}
