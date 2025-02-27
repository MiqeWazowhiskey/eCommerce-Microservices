namespace Basket.API.Basket.AddBasket;

public record AddBasketResult(bool IsSuccess, Guid UserId);
public record AddBasketCommand(ShoppingCart Cart) : ICommand<AddBasketResult>;

public class AddBasketCommandValidator : AbstractValidator<AddBasketCommand>
{
    public AddBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(x=> x.Cart.UserId).NotEmpty().WithMessage("UserId is required");
    }
}

public class AddBasketHandler : ICommandHandler<AddBasketCommand,AddBasketResult>
{
    public async Task<AddBasketResult> Handle(AddBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = command.Cart;
        //todo after repositories
        return new AddBasketResult(true, shoppingCart.UserId);
    }
}