using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnionCartDemo.Domain.Entities;
using System.Collections.Generic;

namespace OnionCartDemo.Infrastructure.Configurations;

internal class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
       builder.ToTable("Carts");

         builder.HasKey(c => c.Id);

        #region  Explanation

        /*
         When EF Core loads a Cart from the database,
         it tries to fill the Items collection with data.
         But Items is read - only — EF Core can’t add to it!
         So it doesn’t know how to store data in the private _items list.
         If you don’t tell EF what to do, you might get errors like:
         “Cannot set navigation property ‘Items’ because it is read-only.”

        */

        builder.Navigation(c => c.Items).UsePropertyAccessMode(PropertyAccessMode.Field);

        #endregion


        builder.HasMany(c => c.Items)
               .WithOne() 
               .HasForeignKey("CartId") 
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade); // When a Cart is deleted, its items are also deleted
    }
}
