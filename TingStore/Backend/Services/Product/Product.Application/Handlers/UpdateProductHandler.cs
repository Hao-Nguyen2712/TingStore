// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Commands;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productUpodate = await this._productRepository.UpdateProduct(new Core.Models.Product
            {
                Id = request.Id,
                Name = request.Name,
                //ProductImage = request.ProductImage,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                UpdateAt = DateTime.Now,
                CategoryId = request.CategoryId,
                IsActive = request.IsActive,
            });
            return productUpodate;
        }
    }
}
