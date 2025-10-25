using OnionCartDemo.Application.DI;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Application.Services;
using OnionCartDemo.Domain.Services;
using OnionCartDemo.Infrastructure.DependencyInjection;
using OnionCartDemo.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();




builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddScoped<ICartDomainService, CartDomainService>();



var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.MapControllers();

app.Run();
