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
      Task DeleteAsync(Product product);
      Task<IEnumerable<ProductDTO>> GetAllAsync(string? sort,int? categoryId,int page_number, int page_size);
    }
}
