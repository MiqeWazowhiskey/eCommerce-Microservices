using Marten.Pagination;

namespace Catalog.API.Features.GetProducts;
public record GetProductsResult(IEnumerable<Product> Products);
public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductsResult>;

internal class GetProductsQueryHandler(IDocumentSession session)
    : IRequestHandler<GetProductsQuery,GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(pageNumber: query.PageNumber ?? 1, pageSize: query.PageSize ?? 10,cancellationToken);

        return new GetProductsResult(products);
    }
}
