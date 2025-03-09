// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Product.Core.Models;

namespace Product.Infrastructure.DbContext
{
    public interface IProductContext
    {
        public IMongoCollection<Product.Core.Models.Product> Products { get; set; }
    }
}
