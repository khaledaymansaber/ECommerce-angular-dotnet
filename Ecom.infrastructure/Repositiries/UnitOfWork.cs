using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositiries
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _context;
        readonly IMapper _mapper;
        private readonly IImageManagmentService _imageManagmentService;
        public IProductRepository Products {  get;  }

        public ICategoryRepositry Categories { get; }

        public IPhotoRepositry Photos { get; }
        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagmentService imageManagmentService)
        {
            _context = context;
            _mapper = mapper;
            _imageManagmentService = imageManagmentService;
            Products = new ProductRepositry(_context, mapper, imageManagmentService);
            Categories = new CategoryRepositry(_context);
            Photos = new PhotoRepositry(_context);
           
        }

    }
}
