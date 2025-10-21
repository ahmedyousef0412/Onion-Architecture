using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Application.Interfaces;

public interface IProductApplicationService
{
    Task<ProductDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto> AddAsync(CreateProductDto dto, CancellationToken cancellationToken = default);
    Task DeactivateAsync(int productId, CancellationToken cancellationToken);

}
