using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositiries.Service
{
    public class ImageManagmentService : IImageManagmentService
    {
        private readonly IFileProvider _fileProvider;
        public ImageManagmentService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            List<string> SaveImageSrc = new List<string>();

            var ImageDirctory = Path.Combine("wwwroot", "Images", src);

            if (Directory.Exists(ImageDirctory) is not true)
            {
                Directory.CreateDirectory(ImageDirctory);
            }

            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    // get Image Name
                    var ImageName = item.FileName;

                    var ImageSrc = $"/Images/{src}/{ImageName}";

                    var root = Path.Combine(ImageDirctory, ImageName);

                    using (FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    SaveImageSrc.Add(ImageSrc);
                }
            }

            return SaveImageSrc;
        }

        public void DeleteImage(string imagePath)
        {
            var info = _fileProvider.GetFileInfo(imagePath);
            var physicalPath = info.PhysicalPath;
            File.Delete(physicalPath);

        }
    }
}
