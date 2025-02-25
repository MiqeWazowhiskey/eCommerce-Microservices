namespace Catalog.API.Features.UpdateProduct;

public record UpdateProductResult(Product Product);
public record UpdateProductCommand(
    Guid Id,
    string? Name,
    string? Description,
    decimal? Price,
    string? ImageFile,
    List<string>? Category) : ICommand<UpdateProductResult>;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x=> x.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(x=>x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger)
    : ICommandHandler<UpdateProductCommand,UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with id {command}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        
        if (command.Name != null)
        {
            product.Name = command.Name;
        }
        
        if (command.Description != null)
        {
            product.Description = command.Description;
        }
        
        if (command.Price != null)
        {
            product.Price = command.Price.Value;
        }
        
        if (command.ImageFile != null)
        {
            product.ImageFile = command.ImageFile;
        }
        
        if (command.Category != null)
        {
            product.Category = command.Category;
        }
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(product);
    }
}
