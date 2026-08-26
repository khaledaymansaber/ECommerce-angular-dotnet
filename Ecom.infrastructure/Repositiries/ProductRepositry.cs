using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
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
        public async Task<IEnumerable<ProductDTO>> GetAllAsync(string? sort,int? categoryId, int page_number, int page_size)
        {
            var query = context.Products
                .Include(m => m.Category)
                .Include(m => m.Photos)
                .AsNoTracking();
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(sort) && _sortStrategies.TryGetValue(sort, out var sortFunc))
            {
                query = sortFunc(query);
            }
            else
            {
                query = query.OrderBy(m => m.Name);
            }
            page_number = page_number <= 0 ? 1 : page_number;
            page_size = page_size <= 0 ? 3 : page_size;
            query = query.Skip((page_number - 1) * page_size).Take(page_size);
            var products = await query.ToListAsync();
            var result = _mapper.Map<List<ProductDTO>>(products);
            return result;
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
