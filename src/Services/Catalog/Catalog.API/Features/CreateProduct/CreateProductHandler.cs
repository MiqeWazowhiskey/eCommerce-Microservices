namespace Catalog.API.Features.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    List<string> Category) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
    }
}
public class CreateProductCommandHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductCommand,CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            ImageFile = command.ImageFile,
            Category = command.Category
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }
}
