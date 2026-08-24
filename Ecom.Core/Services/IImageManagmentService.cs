using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Services
{
    public interface IImageManagmentService
    {
         Task<List<string>> AddImageAsync(IFormFileCollection imageFile, string folderPath);
         void DeleteImage(string imagePath);
    }
}
