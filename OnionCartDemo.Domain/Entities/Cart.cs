using OnionCartDemo.Domain.Exceptions;

namespace OnionCartDemo.Domain.Entities;

public class Cart
{
    public int Id { get;private set; }

    private readonly List<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();


    private Cart() { } // For EF Core

    public static Cart CreateNew()
    {
        return new Cart();
    }

    public decimal TotalAmount => _items.Sum(i => i.TotalPrice);


    public void AddOrUpdateItem(Product product, int quantity) //changes the in-memory cart
    {
        ArgumentNullException.ThrowIfNull(product);

        if (quantity <= 0) throw new InvalidCartItemQuantityException();

        var exitingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);

        exitingItem?.IncreaseQuantity(quantity); // Update quantity

        var item = new CartItem(product.Id, product.UnitPrice, quantity);

        _items.Add(item);
    }
    public CartItem? RemoveItem(int cartItemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == cartItemId);
        if (item is not null)
            _items.Remove(item);

        return item;
    }
    public void Clear() => _items.Clear();

    public void UpdateItemQuantity(int cartItemId, int newQuantity)
    {
        var item = _items.FirstOrDefault(i => i.Id == cartItemId)
            ?? throw new InvalidOperationException($"Item with ID {cartItemId} not found in cart.");

        item.SetQuantity(newQuantity);
    }
}
