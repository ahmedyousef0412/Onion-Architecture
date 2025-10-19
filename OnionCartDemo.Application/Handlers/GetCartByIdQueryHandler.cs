using OnionCartDemo.Application.DTOs;
using OnionCartDemo.Application.Exceptions;
using OnionCartDemo.Application.Interfaces;
using OnionCartDemo.Application.Queries;

namespace OnionCartDemo.Application.Handlers;

public class GetCartByIdQueryHandler
{

    private readonly ICartRepository _cartRepository;

    public GetCartByIdQueryHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<CartDto> HandleAsync(GetCartByIdQuery query ,CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByIdWithItemsAsync(query.CartId,cancellationToken)
            ?? throw new NotFoundException($"Cart with ID {query.CartId} not found.");


        var items = cart.Items.Select(i => new CartItemDto(i.Product.Name, i.UnitPrice, i.Quantity))
            .ToList();

        return new CartDto(cart.Id,items );
       
    }
}
