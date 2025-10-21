using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Application.Exceptions;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Application.Services;

public class ProductApplicationService(IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IProductApplicationService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyCollection<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);

        var productsDtos = products
            .Select(p => new ProductDto(p.Id, p.Name, p.UnitPrice))
            .ToList()
            .AsReadOnly();
        
        return productsDtos;
    }

    public async Task<ProductDto> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        var product = await GetProductAsync(productId, cancellationToken);

        return new ProductDto(product.Id, product.Name, product.UnitPrice);
    }
    
    public async Task<ProductDto> AddAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        var product = new Product(dto.Name, dto.UnitPrice);

         await _productRepository.AddAsync(product, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductDto(product.Id,product.Name, product.UnitPrice);
    }

    public async Task DeactivateAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await GetProductAsync(productId,cancellationToken);

        product.Deactivate();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }


    private  async Task<Product> GetProductAsync(int  productId, CancellationToken cancellationToken = default)
    {
        return await _productRepository.GetByIdAsync(productId, cancellationToken)
                   ?? throw new NotFoundException($"Product with ID {productId} not found.");
    }
}
