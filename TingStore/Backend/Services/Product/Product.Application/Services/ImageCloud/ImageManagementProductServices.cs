using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Product.Application.Interface;

namespace Product.Application.Services.ImageCloud
{
    public class ImageManagementProductServices
    {
        private readonly IImageManagementService _imageManagementService;

        public ImageManagementProductServices(IImageManagementService imageManagementService)
        {
            _imageManagementService = imageManagementService;
        }

        public async Task<string> AddImageProduct(IFormFile file, string id)
        {
            var imageResult = await this._imageManagementService.UploadImage(file, id);
            return imageResult.SecureUrl.ToString();
        }
    }
}
