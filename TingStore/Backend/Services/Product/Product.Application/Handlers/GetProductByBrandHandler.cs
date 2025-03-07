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
    public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByBrandHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Brandname))
            {
                throw new ArgumentNullException("There is an issue with Brandname");
            }
            var productList = await this._productRepository.GetProductByBrand(request.Brandname);
            var productResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productResponseList;
        }
    }
}
