// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Product.Application.Data;
using Product.Application.Interface;

namespace Product.Application.Services.ImageCloud
{
    public class ImageManagementServices : IImageManagementService
    {
        private readonly Cloudinary _cloudinary;
        public ImageManagementServices(IOptions<CloudinarySetting> config)
        {
            var acc = new Account
                (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }
        public Task<DeletionResult> DeleteImage(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
        public async Task<ImageUploadResult> UploadImage(IFormFile file, string Id)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = $"{Id}"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
    }
}
