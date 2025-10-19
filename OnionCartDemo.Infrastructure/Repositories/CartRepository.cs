using Microsoft.EntityFrameworkCore;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Domain.Entities;
using OnionCartDemo.Infrastructure.EntityFramework;

namespace OnionCartDemo.Infrastructure.Repositories;

internal class CartRepository : ICartRepository
{

    private readonly CartDbContext _cartDbContext;

    public CartRepository(CartDbContext cartDbContext)
    {
        _cartDbContext = cartDbContext;
    }
    public async Task<Cart?> GetByIdAsync(int cartId, CancellationToken cancellationToken = default)
    {
        return await _cartDbContext.Carts.FindAsync([cartId], cancellationToken);
    }

    public async Task<Cart?> GetByIdWithItemsAsync(int cartId, CancellationToken cancellationToken = default)
    {
        return await _cartDbContext.Carts
            .Include(c => c.Items)
             .ThenInclude(ci => ci.Product)
             .AsNoTracking()
             .SingleOrDefaultAsync(c => c.Id == cartId,cancellationToken);
    }

    public async Task AddAsync(Cart cart, CancellationToken cancellationToken = default)
    {
         await _cartDbContext.Carts.AddAsync(cart, cancellationToken);
    }

    public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        _cartDbContext.Carts.Update(cart);
    }

    public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        _cartDbContext.Carts.Remove(cart);
    }

   
}
