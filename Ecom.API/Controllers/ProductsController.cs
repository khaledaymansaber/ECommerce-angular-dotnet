using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Shairing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery]ProductParams productParams)
        {
            try
            {
                var products = await unitOfWork.Products.GetAllAsync(productParams);
                
                if (products == null)
                    return BadRequest(new ResponseAPI(400));

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await unitOfWork.Products.GetByIdAsync(id, p => p.Category, p => p.Photos);
                if (product == null)
                    return BadRequest(new ResponseAPI(400));
                var productDto = mapper.Map<ProductDTO>(product);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("add-Product")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDTO AddproductDto)
        {
            try
            {
                
                await unitOfWork.Products.AddAsync(AddproductDto);
                return Ok(new ResponseAPI(200, "Product added successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400,ex.Message));
            }
        }
        [HttpPut("update-Product")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDTO updateProductDto)
        {
            try
            {
                await unitOfWork.Products.UpdateAsync(updateProductDto);
                return Ok(new ResponseAPI(200, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete("delete-Product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await unitOfWork.Products.GetByIdAsync(id, x => x.Photos, x => x.Category);
                await unitOfWork.Products.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product deleted successfully"));
            
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }   
}
