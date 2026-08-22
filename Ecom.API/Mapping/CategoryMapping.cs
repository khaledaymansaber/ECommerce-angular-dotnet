using AutoMapper;
using Ecom.Core;
using Ecom.Core.Entities.Product;
using System.Runtime;

namespace Ecom.API.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

        }
    }
}
