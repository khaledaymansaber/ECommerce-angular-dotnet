using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await unitOfWork.Products.GetAllAsync(p => p.Category , p=> p.Photos);
                if (products == null)
                    return BadRequest(new ResponseAPI(400));
                var productsDto = mapper.Map<IReadOnlyList<ProductDTO>>(products);

                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
