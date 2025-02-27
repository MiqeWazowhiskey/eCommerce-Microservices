namespace Basket.API.Exceptions;

public class BasketNotFoundException(Guid userId) : NotFoundException("Basket", userId.ToString());