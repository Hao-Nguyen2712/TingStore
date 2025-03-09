// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Product.Application.Interface
{
    public interface IImageManagementService
    {
        Task<ImageUploadResult> UploadImage(IFormFile file, string Id);
        Task<DeletionResult> DeleteImage(string publicId);
    }
}
