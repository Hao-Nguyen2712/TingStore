// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Mappers;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Repositories;
using Product.Core.Specs;

namespace Product.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductQuery, Pagination<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetAllProductsHandler> _logger;

        public GetAllProductsHandler(IProductRepository productRepository, ILogger<GetAllProductsHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Pagination<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProducts(request.productSpecParams);
            var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
            _logger.LogDebug("Received Product List.Total Count: {productList}", productResponseList.Count);
            return productResponseList;
        }
    }
}
