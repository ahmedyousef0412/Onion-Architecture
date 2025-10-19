
namespace OnionCartDemo.Application.DTOs;

public record CartDto(int CartId, IReadOnlyCollection<CartItemDto> Items);
