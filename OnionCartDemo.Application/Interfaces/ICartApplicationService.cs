using OnionCartDemo.Application.DTOs;


namespace OnionCartDemo.Application.Interfaces;

public interface ICartApplicationService
{

    Task AddItemAsync(AddItemToCartDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateQuantityAsync(int cartId, int cartItemId, int quantity, CancellationToken cancellationToken);
    Task<CartDto> GetCart(int cartId, CancellationToken cancellationToken);
    Task RemoveItemAsync(int cartId, int cartItemId, CancellationToken cancellationToken);
    Task ClearCartAsync(int cartId, CancellationToken cancellationToken);
    Task DeleteCartAsync(int cartId, CancellationToken cancellationToken);

}
