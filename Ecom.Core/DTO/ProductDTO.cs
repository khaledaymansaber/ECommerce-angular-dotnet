using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO
{
    public record ProductDTO(
        int Id,
        string Name,
        string Description,
        decimal Price,
        string CategoryName,
        IReadOnlyList<PhotoDTO> Photos
    );
    public record AddProductDTO()
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Photos { get; set; } 

    }
    public record UpdateProductDTO(): AddProductDTO
    {
        public int Id { get; set; }
    }
}
