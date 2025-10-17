
namespace OnionCartDemo.Domain.Exceptions;

public class ProductNameRequiredException:DomainException
{
    public ProductNameRequiredException() : base("Product name is required.") { }
}
