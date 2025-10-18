using OnionCartDemo.Domain.Exceptions;

namespace OnionCartDemo.Domain.Entities;

public class CartItem
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; } //price at the time the item was added to the cart
    private CartItem() { } // For EF Core


    public CartItem(int productId, decimal unitPrice,int quantity)
    {

        if (quantity < 1)
            throw new InvalidCartItemQuantityException();

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
    public decimal TotalPrice => UnitPrice * Quantity;  // 100 * 2 = 200


    public void IncreaseQuantity(int amount)
    {
        if (amount < 1)
            throw new InvalidCartItemQuantityException();

        Quantity += amount;
    }

    public void SetQuantity(int quantity)
    {
        if (quantity < 1)
            throw new InvalidCartItemQuantityException();

        Quantity = quantity;
    }
}
