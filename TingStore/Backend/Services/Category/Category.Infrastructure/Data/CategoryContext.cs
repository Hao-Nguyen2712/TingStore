// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Category.Infrastructure.Data
{
    public class CategoryContext : ICategoryContext
    {
        public IMongoCollection<Category.Core.Entities.Category> Categories { get; set; }

        public CategoryContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Categories = database.GetCollection<Category.Core.Entities.Category>(configuration.GetValue<string>("DatabaseSettings:CategoryCollectionName"));
        }
    }
}
