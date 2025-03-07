// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Mappers;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productList = await this._productRepository.GetProduct(request.Id);
            var productResponseList = ProductMapper.Mapper.Map<ProductResponse>(productList);
            return productResponseList;
        }
    }
}
