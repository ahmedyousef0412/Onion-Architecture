namespace OnionCartDemo.Application.DTOs;

public record AddItemToCartDto(
    int CartId,
    int ProductId,
    int Quantity
);

