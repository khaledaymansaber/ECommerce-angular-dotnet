using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(c => c.Name)
                   .IsRequired();

            builder.Property(c => c.Description)
                   .IsRequired();

            builder.Property(c => c.NewPrice)
                   .HasColumnType("decimal(18,2)");


            builder.HasOne(p => p.Category)
                 .WithMany(c => c.Products)
                 .HasForeignKey(p => p.CategoryId);
            builder.HasData(
                new Product { Id = 1, Name = "Smartphone", Description = "Latest smartphone with advanced features", OldPrice = 799.99m, NewPrice = 699.99m, CategoryId = 1 },
new Product { Id = 2, Name = "Laptop", Description = "High-performance laptop for work and gaming", OldPrice = 1499.99m, NewPrice = 1299.99m, CategoryId = 1 },
new Product { Id = 3, Name = "T-shirt", Description = "Comfortable cotton t-shirt in various sizes", OldPrice = 29.99m, NewPrice = 19.99m, CategoryId = 2 },
new Product { Id = 4, Name = "Blender", Description = "Powerful blender for smoothies and food preparation", OldPrice = 109.99m, NewPrice = 89.99m, CategoryId = 3 },
new Product { Id = 5, Name = "Running Shoes", Description = "Lightweight running shoes for all terrains", OldPrice = 99.99m, NewPrice = 79.99m, CategoryId = 5 }
            );
        }
    }
}
