using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);

}
