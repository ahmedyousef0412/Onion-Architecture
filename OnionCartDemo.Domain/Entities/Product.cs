using OnionCartDemo.Domain.Exceptions;

namespace OnionCartDemo.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal UnitPrice { get; private set; }

    public bool IsActive { get;  private set; }
    private Product() { } // For EF Core


    public Product(string name , decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ProductNameRequiredException();

        if (unitPrice < 0)
            throw new ProductPriceNegativeException();

        Name = name;
        UnitPrice = unitPrice;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ProductPriceNegativeException();
        UnitPrice = newPrice;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new ProductAlreadyInactiveException();

        IsActive = false;
    }


}
