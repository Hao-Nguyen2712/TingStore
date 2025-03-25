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

namespace Product.Application.Handlers
{
    public class GetAllNFProductsHandler : IRequestHandler<GetAllNFProductQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetAllProductsHandler> _logger;

        public GetAllNFProductsHandler(IProductRepository productRepository, ILogger<GetAllProductsHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<ProductResponse>> Handle(GetAllNFProductQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProduct();
            var productResponseList = ProductMapper.Mapper.Map<IEnumerable<ProductResponse>>(productList);
            //_logger.LogDebug("Received Product List.Total Count: {productList}", productResponseList.Count);
            return productResponseList;
        }
    }
}
