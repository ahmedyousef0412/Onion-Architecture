namespace OnionCartDemo.Application.Commands;

public record AddItemToCartCommand(
    int CartId,
    int ProductId,
    int Quantity
);

