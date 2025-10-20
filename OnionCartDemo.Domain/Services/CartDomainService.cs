using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Domain.Services;

#region Explanation

//A Domain Service (CartDomainService ) exists to hold business rules or logic that:
//Don’t naturally belong to any single entity(like Cart or Product).
//Need multiple entities to make a decision together
//both a Cart and a Product — not just one.

#endregion

public class CartDomainService : ICartDomainService
{
    public void ValidateCanAddItem(Cart cart, Product product, int quantity)
    {
        if (quantity < 1)
            throw new ArgumentException("Quantity must be at least 1.", nameof(quantity));

        ArgumentNullException.ThrowIfNull(product);

        //if(quantity > product.Stock)
        //    throw new DomainException("Not enough stock.");

        //7000 =                    5000             + ( 1000 * 2 )
        var projectedTotal = cart.TotalAmount + (product.UnitPrice * quantity);

        const decimal maxCartTotal = 10000m; 

        if (projectedTotal > maxCartTotal)
            throw new InvalidOperationException($"Adding this item exceeds the maximum cart total of {maxCartTotal:C}.");
    }
}
