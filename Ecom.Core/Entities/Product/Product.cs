using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entities.Product
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
