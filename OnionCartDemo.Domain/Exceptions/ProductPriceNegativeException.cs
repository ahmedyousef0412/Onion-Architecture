
namespace OnionCartDemo.Domain.Exceptions;

public class ProductPriceNegativeException : DomainException
{
    public ProductPriceNegativeException() : base("Unit price cannot be negative.") { }
    
}
