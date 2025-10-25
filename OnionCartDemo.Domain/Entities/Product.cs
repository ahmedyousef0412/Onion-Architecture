using OnionCartDemo.Domain.Exceptions;

namespace OnionCartDemo.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal UnitPrice { get; private set; }

    public bool IsActive { get;  private set; }

    public string? ImageUrl { get; private set; }
    private Product() { } // For EF Core


    public Product(string name , decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required.");

        if (unitPrice < 0)
            throw new DomainException("Unit price cannot be negative.");

        Name = name;
        UnitPrice = unitPrice;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new DomainException("Unit price cannot be negative.");

        UnitPrice = newPrice;
    }

    public void Deactivate()
    {
        if (!IsActive)

            throw new DomainException("Product is already inactive.");

        IsActive = false;
    }

    public void SetImageUrl(string imageUrl)
    {

        //UriKind.Absolute => https://example.com/image.jpg
        if (!string.IsNullOrWhiteSpace(imageUrl) &&
            !Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
              throw new DomainException("Invalid image URL format.");

      ImageUrl = imageUrl;
    }
}
