using Microsoft.Extensions.DependencyInjection;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Application.Services;
using OnionCartDemo.Application.Validation;


namespace OnionCartDemo.Application.DI;

public static class ApplicationServiceRegistration
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductApplicationService, ProductApplicationService>();

        services.AddScoped<ICartApplicationService, CartApplicationService>();

        services.AddScoped<FileUploadDtoValidator>();
      
        return services;
    }
}
