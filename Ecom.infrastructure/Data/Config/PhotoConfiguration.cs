using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Data.Config
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {

            builder.HasOne(p => p.Product)
               .WithMany(p => p.Photos)
               .HasForeignKey(p => p.ProductId);
            builder.HasData(
                new Photo { Id = 1, ImageName = "product-1.jpg", ProductId = 1 },
                new Photo { Id = 2, ImageName = "product-2.jpg", ProductId = 2 },
                new Photo { Id = 3, ImageName = "product-3.jpg", ProductId = 3 },
                new Photo { Id = 4, ImageName = "product-4.jpg", ProductId = 4 },
                new Photo { Id = 5, ImageName = "product-5.jpg", ProductId = 5 }
            );
        }
    }
}
