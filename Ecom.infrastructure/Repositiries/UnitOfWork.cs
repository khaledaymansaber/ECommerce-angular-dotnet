using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositiries
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _context;
        public IProductRepository Products {  get;  }

        public ICategoryRepositry Categories { get; }

        public IPhotoRepositry Photos { get; }
        public UnitOfWork(AppDbContext context )
        {
          _context = context;
            Products = new ProductRepositry(_context);
            Categories = new CategoryRepositry(_context);
            Photos = new PhotoRepositry(_context);
        }

    }
}
