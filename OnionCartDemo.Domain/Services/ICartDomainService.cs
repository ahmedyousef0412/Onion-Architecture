using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Domain.Services;

public interface ICartDomainService
{
    void ValidateCanAddItem(Cart cart, Product product, int quantity);
}
