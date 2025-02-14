using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Features.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    List<string> Category) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);
public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand,CreateProductResult>
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
        
        //TODO: save db edit result
        
        return new CreateProductResult(Guid.NewGuid());
    }
}
