namespace OnionCartDemo.Domain.Exceptions;

public sealed  class InvalidCartItemQuantityException:DomainException
{
    public InvalidCartItemQuantityException() : base("Quantity must be greater than zero.") { }
}
