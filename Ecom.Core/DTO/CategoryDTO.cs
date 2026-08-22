using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core
{
   public record CategoryDTO(string Name, string Description);
   public record UpdateCategoryDTO(string Name, string Description,int id);

}
