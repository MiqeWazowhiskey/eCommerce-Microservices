using BuildingBlocks.Exceptions;

namespace Catalog.API.Features.GetProductByCategory;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);
public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public class GetProductsByCategoryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync();
        if (products is null)
        {
            throw new NotFoundException("Products not found with related category.");
        }
        
        return new GetProductsByCategoryResult(products);
    }
}
