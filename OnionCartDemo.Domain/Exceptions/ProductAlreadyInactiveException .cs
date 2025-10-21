
namespace OnionCartDemo.Domain.Exceptions;

public sealed class ProductAlreadyInactiveException:DomainException
{
    public ProductAlreadyInactiveException(): base("Product is already inactive.") { }
}
