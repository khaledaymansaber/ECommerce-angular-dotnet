using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class PhotoMapping:Profile
    {
            public PhotoMapping()
            {
            CreateMap<Photo, PhotoDTO>();
        }
    }
}
