using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Application.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(int cartId, CancellationToken cancellationToken  = default);
    Task<Cart?> GetByIdWithItemsAsync(int cartId, CancellationToken cancellationToken  = default);
    Task<Cart?> GetByIdWithItemsAsNoTrackingAsync(int cartId, CancellationToken cancellationToken  = default);
    Task AddAsync(Cart cart, CancellationToken cancellationToken  = default);
    void Delete(Cart cart);
    void RemoveItem(CartItem item);
}
