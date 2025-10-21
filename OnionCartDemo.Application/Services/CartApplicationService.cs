using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Application.Exceptions;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Domain.Entities;
using OnionCartDemo.Domain.Services;


namespace OnionCartDemo.Application.Services;

public class CartApplicationService(ICartRepository cartRepository,
    IProductRepository productRepository,
    ICartDomainService cartDomainService,
    IUnitOfWork unitOfWork) : ICartApplicationService
{
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ICartDomainService _cartDomainService = cartDomainService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    //AddItem
    public async Task AddItemAsync(AddItemToCartDto dto ,CancellationToken cancellationToken)
    {
        //Check if cart exists before

        var cart = await _cartRepository.GetByIdAsync(dto.CartId, cancellationToken);

        var product = await _productRepository.GetByIdAsync(dto.ProductId, cancellationToken)
                  ?? throw new NotFoundException($"Product with Id {dto.ProductId} not found.");


        if (cart is null)
        {
            cart = Cart.CreateNew();

            cart.AddOrUpdateItem(product, dto.Quantity); //Check domain rule 
            await _cartRepository.AddAsync(cart, cancellationToken); //Persist the db
        }

        else
        {
            _cartDomainService.ValidateCanAddItem(cart, product, dto.Quantity);
            cart.AddOrUpdateItem(product, dto.Quantity);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }



    #region Return the Cart Id => frontend needs to know which cart was updated or created
    //AddItem
    //public async Task<int> AddItemAsync(AddItemToCartDto dto, CancellationToken cancellationToken)
    //{
    //    //Check if cart exists before

    //    var cart = await _cartRepository.GetByIdAsync(dto.CartId, cancellationToken);

    //    var product = await _productRepository.GetByIdAsync(dto.ProductId, cancellationToken)
    //              ?? throw new NotFoundException($"Product with Id {dto.ProductId} not found.");


    //    if (cart is null)
    //    {
    //        cart = Cart.CreateNew();

    //        cart.AddOrUpdateItem(product, dto.Quantity); //Check domain rule 
    //        await _cartRepository.AddAsync(cart, cancellationToken); //Persist the db
    //    }

    //    else
    //    {
    //        _cartDomainService.ValidateCanAddItem(cart, product, dto.Quantity);
    //        cart.AddOrUpdateItem(product, dto.Quantity);
    //    }

    //    await _unitOfWork.SaveChangesAsync(cancellationToken);
    //    return cart.Id;
    //}

    #endregion



    //UpdateQuantiyOfItem

    public async Task<bool> UpdateQuantityAsync(int cartId, int cartItemId,int quantity , CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdWithItemsAsync(cartId, cancellationToken)
            ?? throw new NotFoundException($"Cart with Id {cartId} not found");

        
        cart.UpdateItemQuantity(cartItemId, quantity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }


    //GetCartWithRelatedItems
    public async Task<CartDto> GetCart(int cartId,CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdWithItemsAsync(cartId, cancellationToken)
           ?? throw new NotFoundException($"Cart with ID {cartId} not found.");

        var items = cart.Items
            .Select(i => new CartItemDto(i.Product.Name, i.UnitPrice, i.Quantity))
            .ToList();

        return new CartDto(cartId, items);
    }

    //RemoveItem
    public async Task RemoveItemAsync(int cartId, int cartItemId, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdWithItemsAsync(cartId, cancellationToken)
            ?? throw new NotFoundException($"Cart with Id {cartId} not found");

        var item = cart.RemoveItem(cartItemId);

        if (item is not null)
            _cartRepository.RemoveItem(item);


        await _unitOfWork.SaveChangesAsync( cancellationToken);
    }



    //Clear Cart Items
    public async Task ClearCartAsync(int cartId, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdWithItemsAsync(cartId, cancellationToken)
                   ?? throw new NotFoundException($"Cart with ID {cartId} not found.");

        cart.Clear();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }


    //Delete Cart
    public async Task DeleteCartAsync(int cartId, CancellationToken cancellationToken)
    {
        
        var cart = await _cartRepository.GetByIdWithItemsAsync(cartId, cancellationToken)
                   ?? throw new NotFoundException($"Cart with ID {cartId} not found.");

        _cartRepository.Delete(cart); 

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
