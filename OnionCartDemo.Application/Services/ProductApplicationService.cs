using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Application.Exceptions;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Application.Validation;
using OnionCartDemo.Domain.Entities;
using System.ComponentModel.DataAnnotations;



namespace OnionCartDemo.Application.Services;

public class ProductApplicationService(IProductRepository productRepository
    ,IImageStorageService imageStorageService,
    FileUploadDtoValidator validator,
    IUnitOfWork unitOfWork) : IProductApplicationService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IImageStorageService _imageStorageService = imageStorageService;
    private readonly FileUploadDtoValidator _validator = validator;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyCollection<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);

        var productsDtos = products
            .Select(p => new ProductDto(p.Id, p.Name, p.UnitPrice ,p.ImageUrl))
            .ToList()
            .AsReadOnly();
        
        return productsDtos;
    }

    public async Task<ProductDto> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        var product = await GetProductAsync(productId, cancellationToken);

        return new ProductDto(product.Id, product.Name, product.UnitPrice,product.ImageUrl);
    }
    
    public async Task<ProductDto> AddAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        var product = new Product(dto.Name, dto.UnitPrice);

         await _productRepository.AddAsync(product, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductDto(product.Id,product.Name, product.UnitPrice,product.ImageUrl);
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

    public async Task<ProductDto> UploadProductImageAsync(int productId,FileUploadDto file, CancellationToken cancellationToken = default)
    {
        var product = await GetProductAsync(productId, cancellationToken);

        var validationResult = _validator.Validate(file);

        if (validationResult is not null)
        {
            throw new ValidationException(validationResult.ToString());
        }


        var imageUrl =  await _imageStorageService.UploadAsync(file, cancellationToken);

        product.SetImageUrl(imageUrl);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductDto(product.Id, product.Name, product.UnitPrice, product.ImageUrl);

    }


    #region Before using FluentValidation
    //private static void ValidateImage(FileUploadDto file)
    //{
    //    const long maxSize = 2 * 1024 * 1024;

    //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
    //    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();



    //    if (file is null || file.Content is null || file.Content.Length == 0)
    //        throw new ApplicationException("Image file is required.");


    //    if (file.Content.Length > maxSize)
    //        throw new ApplicationException("Image size cannot exceed 2 MB.");


    //    if (!allowedExtensions.Contains(extension))
    //        throw new ApplicationException("Only JPG and PNG formats are allowed.");
    //} 
    #endregion
}
