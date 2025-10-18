namespace OnionCartDemo.Domain.Entities;

public class Cart
{
    public int Id { get;private set; }

    private readonly List<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();


    private Cart() { } // For EF Core

    public Cart(int id) //Used for referencing an existing cart (without reloading)
    {
        Id = id;
    }

    public decimal TotalAmount => _items.Sum(i => i.TotalPrice);


    public void AddOrUpdateItem(Product product, int quantity)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (quantity <= 0) throw new ArgumentException("Quantity must be > 0", nameof(quantity));

        var exitingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);

        exitingItem?.IncreaseQuantity(quantity); // Update quantity

        var item = new CartItem(product.Id, product.UnitPrice, quantity);

        _items.Add(item);
    }

    public void RemoveItem(int cartItemId) 
    {
        var item = _items.FirstOrDefault(i => i.Id == cartItemId);

        if (item is not null)
         _items.Remove(item);
        
    }
}
