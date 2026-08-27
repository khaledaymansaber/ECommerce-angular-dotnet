using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Core.Shairing;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositiries.Service;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositiries
{
    public class ProductRepositry : GenericRepositry<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext context;
        private readonly IImageManagmentService imageManagmentService;
        public ProductRepositry(AppDbContext context, IMapper mapper, IImageManagmentService imageManagmentService) : base(context)
        {
            _mapper = mapper;
            this.context = context;
            this.imageManagmentService = imageManagmentService;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null) return false;

           
            var product = _mapper.Map<Product>(productDTO);

            
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

         
            List<string> ImagePath = await imageManagmentService.AddImageAsync(productDTO.Photos, productDTO.Name);

            
            List<Photo> photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id,
            }).ToList();

            
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            var photo = await context.Photos.Where(p => p.ProductId == product.Id).ToListAsync();
            foreach (var p in photo)
            {
                imageManagmentService.DeleteImage(p.ImageName);
            }
            context.Products.Remove(product);
           await context.SaveChangesAsync();
        }
        public static readonly Dictionary<string, Func<IQueryable<Product>, IOrderedQueryable<Product>>> _sortStrategies =
            new(StringComparer.OrdinalIgnoreCase)
        {
            { "PriceAsn", q => q.OrderBy(m => m.NewPrice) },
            { "PriceDes", q => q.OrderByDescending(m => m.NewPrice) },
            { "Name", q => q.OrderBy(m => m.Name) }
        };
        public async Task<Pagination<ProductDTO>> GetAllAsync(ProductParams productParams)
        {
            var query = context.Products
                .Include(m => m.Category)
                .Include(m => m.Photos)
                .AsNoTracking();

            if (productParams.CategoryId.HasValue && productParams.CategoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == productParams.CategoryId.Value);
            }

            if (!string.IsNullOrEmpty(productParams.Sort) && _sortStrategies.TryGetValue(productParams.Sort, out var sortFunc))
            {
                query = sortFunc(query);
            }
            else
            {
                query = query.OrderBy(m => m.Name);
            }
            var totalItems = await query.CountAsync();
            query = query.Skip((productParams.PageNumber - 1) * productParams.pageSize).Take(productParams.pageSize);

            var products = await query.ToListAsync();
            var mappedData = _mapper.Map<List<ProductDTO>>(products);

            return new Pagination<ProductDTO>(totalItems ,productParams.PageNumber, productParams.pageSize,mappedData);
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO == null)
            {
                return false;
            }

            var existingProduct = await context.Products
                .Include(m => m.Category)
                .Include(m => m.Photos)
                .FirstOrDefaultAsync(m => m.Id == updateProductDTO.Id);

            if (existingProduct == null)
            {
                return false;
            }

            _mapper.Map(updateProductDTO, existingProduct);

            if (updateProductDTO.Photos != null && updateProductDTO.Photos.Count > 0)
            {
                foreach (var oldPhoto in existingProduct.Photos)
                {
                     imageManagmentService.DeleteImage(oldPhoto.ImageName);
                }

                context.Photos.RemoveRange(existingProduct.Photos);

                var newImagePaths = await imageManagmentService.AddImageAsync(updateProductDTO.Photos, updateProductDTO.Name);

                existingProduct.Photos = newImagePaths.Select(path => new Photo
                {
                    ImageName = path,
                    ProductId = existingProduct.Id
                }).ToList();
            }

            await context.SaveChangesAsync();

            return true;
        }
        
      



    }
}
