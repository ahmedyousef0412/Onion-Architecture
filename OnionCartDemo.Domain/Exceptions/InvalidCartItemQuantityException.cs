namespace OnionCartDemo.Domain.Exceptions;

public class InvalidCartItemQuantityException:DomainException
{
    public InvalidCartItemQuantityException() : base("Cart item quantity must be at least 1.") { }
}
