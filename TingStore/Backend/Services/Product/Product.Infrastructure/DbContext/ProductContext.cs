// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Product.Core.Models;

namespace Product.Infrastructure.DbContext
{
    public class ProductContext : IProductContext
    {
        public IMongoCollection<Core.Models.Product> Products { get; set; }

        public ProductContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product.Core.Models.Product>(configuration.GetValue<string>("DatabaseSettings:ProductCollectionName"));
        }
    }
}
