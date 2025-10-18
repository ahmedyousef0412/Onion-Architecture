using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Domain.Entities;
using OnionCartDemo.Domain.Services;

namespace OnionCartDemo.Application.Services;

public class CartApplicationService
{

    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICartDomainService _cartDomainService;

    public CartApplicationService(ICartRepository cartRepository, IProductRepository productRepository, ICartDomainService cartDomainService)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _cartDomainService = cartDomainService;
    }

    public  async Task AddItemAsync(AddItemToCartRequest request, CancellationToken cancellationToken = default)
    {
        //Check if cart exists
        var cart = await _cartRepository.GetByIdAsync(request.CartId, cancellationToken);

        if (cart is null)
        {
            cart = new Cart(request.CartId);
            await _cartRepository.AddAsync(cart, cancellationToken);
        }

        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new Exception("Product not found");


        //Validate domain rules => Using Domain Service
        _cartDomainService.ValidateCanAddItem(cart, product, request.Quantity);

        cart.AddOrUpdateItem(product,request.Quantity);

        await _cartRepository.Update(cart, cancellationToken);
    }
}
