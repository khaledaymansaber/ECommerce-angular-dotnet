using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Ecom.API.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await unitOfWork.Categories.GetAllAsync();
                if (categories == null)
                    return BadRequest(new ResponseAPI(400));
                return Ok(categories);
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
                var categorie = await unitOfWork.Categories.GetByIdAsync(id);
                if (categorie == null)
                return BadRequest(new ResponseAPI(400, $"Not Found Category id={id}"));
                return Ok(categorie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var Category = mapper.Map<Category>(categoryDTO);
                await unitOfWork.Categories.AddAsync(Category);
                return Ok(new ResponseAPI(200,"item has been added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO categoryDTO)
          {
            try
            {
                var Category = mapper.Map<Category>(categoryDTO);
                await unitOfWork.Categories.UpdateAsync(Category);
                
                return Ok(new ResponseAPI(200, "item has been updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          }
            [HttpDelete("delete-category/{id}")]

             public async Task<IActionResult> DeleteCategory(int id)
              {
                try
                {
                    var category = await unitOfWork.Categories.GetByIdAsync(id);
                    if (category == null)
                        return BadRequest("No category found.");
                    await unitOfWork.Categories.DeleteAsync(id);
                    return Ok(new ResponseAPI(200, "item has been deleted"));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

              }
    }
}
