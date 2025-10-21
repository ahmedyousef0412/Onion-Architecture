
namespace OnionCartDemo.Domain.Exceptions;

public sealed class ProductPriceNegativeException : DomainException
{
    public ProductPriceNegativeException() : base("Unit price cannot be negative.") { }
    
}
