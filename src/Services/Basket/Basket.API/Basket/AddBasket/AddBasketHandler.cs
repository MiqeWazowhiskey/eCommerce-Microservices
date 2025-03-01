using Discount.Grpc;

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

public class AddToBasketCommandHandler(IBasketRepository repository,DiscountProtoService.DiscountProtoServiceClient client)
    : ICommandHandler<AddBasketCommand,AddBasketResult>
{
    public async Task<AddBasketResult> Handle(AddBasketCommand command, CancellationToken cancellationToken)
    {

        await DeductDiscount(command.Cart, cancellationToken);
        //basket can be null if null create if not update (marten)
        await repository.AddBasketAsync(command.Cart, cancellationToken);
        return new AddBasketResult(true, command.Cart.UserId);
    }
    
    public async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await client.GetDiscountAsync(new GetDiscountRequest() { ProductId = item.ProductId.ToString() });
            item.Price -= coupon.Amount;
        }
    }
}