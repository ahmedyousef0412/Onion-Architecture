using OnionCartDemo.Application.Commands;
using OnionCartDemo.Application.Exceptions;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Domain.Entities;
using OnionCartDemo.Domain.Services;

namespace OnionCartDemo.Application.Handlers;

public sealed class AddItemToCartCommandHandler
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICartDomainService _cartDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public AddItemToCartCommandHandler(ICartRepository cartRepository,
        IProductRepository productRepository,
        ICartDomainService cartDomainService,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _cartDomainService = cartDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(AddItemToCartCommand command , CancellationToken cancellationToken = default)
    {
        //Check if cart exists before

        var cart = await _cartRepository.GetByIdAsync(command.CartId, cancellationToken);

        var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken)
                  ?? throw new NotFoundException($"Product with Id {command.ProductId} not found.");


        if(cart is null)
        {
            cart = Cart.CreateNew();
            cart.AddOrUpdateItem(product, command.Quantity); //Check domain rule 
            await _cartRepository.AddAsync(cart,cancellationToken); //Persist the db
        }

        else
        {
            _cartDomainService.ValidateCanAddItem(cart,product,command.Quantity);
            cart.AddOrUpdateItem(product,command.Quantity);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
