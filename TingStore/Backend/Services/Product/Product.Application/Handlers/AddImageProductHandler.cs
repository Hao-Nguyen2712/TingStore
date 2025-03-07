// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Commands;
using Product.Application.Responses;
using Product.Application.Services.ImageCloud;
using Product.Core.Repositories;
using Product.Core.Specs;

namespace Product.Application.Handlers
{
    public class AddImageProductHandler : IRequestHandler<AddImageProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ImageManagementProductServices _imageManagementProductServices;

        public AddImageProductHandler(IProductRepository productRepository, ImageManagementProductServices imageManagementProductServices)
        {
            _productRepository = productRepository;
            _imageManagementProductServices = imageManagementProductServices;
        }

        public async Task<bool> Handle(AddImageProductCommand request, CancellationToken cancellationToken)
        {
            string fileId = await this._imageManagementProductServices.AddImageProduct(request.imageFile, "p");
            var result = await _productRepository.AddImageToProduct(new ImageItem
            {
                id = request.id,
                imageUrl = fileId,
                isPrimary = request.isPrimary
            });

            return result;
        }
    }
}
