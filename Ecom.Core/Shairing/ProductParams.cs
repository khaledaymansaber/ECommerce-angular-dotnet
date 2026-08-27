using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Shairing
{
    public class ProductParams
    {
        public string? Sort { get; set; } 
        public int? CategoryId { get; set; }
        public int PageNumber { get; set; }=1;
        public int MaxPageSize { get; set; } = 6;
        private int PageSize = 3;
        public int pageSize
        {
            get { return PageSize; }
            set { PageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
