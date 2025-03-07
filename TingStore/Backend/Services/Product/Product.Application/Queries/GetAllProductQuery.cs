// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Responses;
using Product.Core.Specs;

namespace Product.Application.Queries
{
    public class GetAllProductQuery : IRequest<Pagination<ProductResponse>>
    {
        public ProductSpecParams productSpecParams { get; set; }

        public GetAllProductQuery(ProductSpecParams productSpecParams) => this.productSpecParams = productSpecParams;
    }
}
