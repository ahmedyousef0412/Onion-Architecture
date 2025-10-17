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


    internal void AddOrUpdateItem(CartItem item)
    {
        var exitingItem = _items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (exitingItem is not null)
        {
            exitingItem.IncreaseQuantity(item.Quantity); // Update quantity
        }
        else
        {
            _items.Add(item);
        }
    }

    internal void RemoveItem(int productId) 
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);

        if (item is not null)
         _items.Remove(item);
        
    }
}
