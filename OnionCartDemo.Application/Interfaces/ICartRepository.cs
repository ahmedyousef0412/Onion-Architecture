using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Application.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(int cartId, CancellationToken cancellationToken  = default);
    Task AddAsync(Cart cart, CancellationToken cancellationToken  = default);
    Task Update(Cart cart, CancellationToken cancellationToken  = default);
}
