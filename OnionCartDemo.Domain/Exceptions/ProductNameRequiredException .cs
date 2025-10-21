
namespace OnionCartDemo.Domain.Exceptions;

public sealed class ProductNameRequiredException:DomainException
{
    public ProductNameRequiredException() : base("Product name is required.") { }
}
