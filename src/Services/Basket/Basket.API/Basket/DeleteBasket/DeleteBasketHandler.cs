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

public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        //todo after repositories
        throw new NotImplementedException();
    }
}