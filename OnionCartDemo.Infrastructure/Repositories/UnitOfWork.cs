using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Infrastructure.EntityFramework;

namespace OnionCartDemo.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly CartDbContext _cartDbContext;
    public UnitOfWork(CartDbContext cartDbContext)
    {
        _cartDbContext = cartDbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await  _cartDbContext.SaveChangesAsync(cancellationToken);
    }
}
