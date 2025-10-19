namespace OnionCartDemo.Domain.Exceptions;

public class InvalidCartItemQuantityException:DomainException
{
    public InvalidCartItemQuantityException(string message) : base(message) { }
}
