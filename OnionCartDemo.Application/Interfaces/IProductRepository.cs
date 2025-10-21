using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsNoTrackingAsync(int productId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
}
