using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Application.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(int cartId, CancellationToken cancellationToken  = default);
    Task AddAsync(Cart cart, CancellationToken cancellationToken  = default);
    Task UpdateAsync(Cart cart, CancellationToken cancellationToken  = default);
    Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default);
}
