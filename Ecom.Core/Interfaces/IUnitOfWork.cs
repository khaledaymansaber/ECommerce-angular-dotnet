using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; }
        public ICategoryRepositry Categories { get; }
        public IPhotoRepositry Photos { get; }
    }
}
