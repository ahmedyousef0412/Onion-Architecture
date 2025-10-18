using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Infrastructure.EntityFramework;
using OnionCartDemo.Infrastructure.Repositories;

namespace OnionCartDemo.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CartDbContext>(options =>
             options.UseSqlServer(connectionString));

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<ICartRepository, CartRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();



        return services;
    }
}
