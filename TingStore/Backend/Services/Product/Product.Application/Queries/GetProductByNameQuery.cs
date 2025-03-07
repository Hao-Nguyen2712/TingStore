// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Responses;

namespace Product.Application.Queries
{
    public class GetProductByNameQuery : IRequest<IList<ProductResponse>>
    {
        public string Name { get; set; }

        public GetProductByNameQuery(string name)
        {
            Name = name;
        }
    }
}
