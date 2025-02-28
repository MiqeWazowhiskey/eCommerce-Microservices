namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResult(bool IsSuccess);
public record DeleteBasketCommand(Guid UserId) : ICommand<DeleteBasketResult>;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}

public class DeleteBasketQueryHandler(IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteBasketAsync(request.UserId, cancellationToken);
        return new DeleteBasketResult(result);
    }
}