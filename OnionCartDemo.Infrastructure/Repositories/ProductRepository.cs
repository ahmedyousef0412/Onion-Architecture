using Microsoft.EntityFrameworkCore;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Domain.Entities;
using OnionCartDemo.Infrastructure.EntityFramework;

namespace OnionCartDemo.Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{

    private readonly CartDbContext _cartDbContext;

    public ProductRepository(CartDbContext cartDbContext )
    {
        _cartDbContext = cartDbContext;
    }

    public async Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        #region Include InActive Products 
        //var query = _cartDbContext.Products.AsNoTracking();

        //if(onlyActive)
        //    query = query.Where(p => p.IsActive);

        //return await query.ToListAsync(cancellationToken);
        #endregion

        return await _cartDbContext.Products
            .AsNoTracking()
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken); //materialize into memory

    }

    public async Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _cartDbContext.Products
                                   .FindAsync(productId, cancellationToken);
    }
  
    public async Task<Product?> GetByIdAsNoTrackingAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _cartDbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
       await _cartDbContext.Products.AddAsync(product, cancellationToken);
    }

}
