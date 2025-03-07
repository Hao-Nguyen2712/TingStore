// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Commands;
using Product.Application.Mappers;
using Product.Application.Responses;
using Product.Application.Services.ImageCloud;
using Product.Core.Models;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ImageManagementProductServices _imageManagementProductServices;

        public CreateProductHandler(IProductRepository productRepository, ImageManagementProductServices imageManagementProductServices)
        {
            _productRepository = productRepository;
            _imageManagementProductServices = imageManagementProductServices;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            string fileId = await this._imageManagementProductServices.AddImageProduct(request.ProductFile, "p");
            var productEntity = ProductMapper.Mapper.Map<Product.Core.Models.Product>(request);
            productEntity.CreateAt = DateTime.Now;
            productEntity.UpdateAt = DateTime.Now;
            productEntity.ProductImage = fileId;
            if (productEntity is null)
            {
                throw new ArgumentNullException("There is an issue with mapping while creating new product");
            }
            var newProduct = await _productRepository.CreateProduct(productEntity);
            var productResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
            return productResponse;
        }
    }
}
