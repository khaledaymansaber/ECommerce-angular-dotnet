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
}
