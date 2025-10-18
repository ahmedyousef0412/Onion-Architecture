namespace OnionCartDemo.Application.DTOs;

public record AddItemToCartRequest(
    int CartId,
    int ProductId,
    int Quantity
);

