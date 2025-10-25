using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Infrastructure.Configurations;
using OnionCartDemo.Infrastructure.EntityFramework;
using OnionCartDemo.Infrastructure.Repositories;
using OnionCartDemo.Infrastructure.Services.CloudinaryService;


namespace OnionCartDemo.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


        services.AddDbContext<CartDbContext>(options =>
             options.UseSqlServer((connectionString)));

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<ICartRepository, CartRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        // Register Cloudinary configuration
        //services.Configure<CloudinarySettings>(configuration.GetSection(nameof(CloudinarySettings)));

        services.AddOptions<CloudinarySettings>()
            .Bind(configuration.GetSection(nameof(CloudinarySettings)))
            .ValidateOnStart();

        services.AddScoped<IImageStorageService,CloudinaryImageStorageService>();


        return services;
    }
}
