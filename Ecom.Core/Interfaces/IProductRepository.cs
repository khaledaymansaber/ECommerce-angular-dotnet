using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository : IGenericRepositry<Product>
    {
      Task<bool> AddAsync(AddProductDTO AddproductDTO);
      Task<bool> UpdateAsync(UpdateProductDTO UpdateProductDTO);
    }
}
