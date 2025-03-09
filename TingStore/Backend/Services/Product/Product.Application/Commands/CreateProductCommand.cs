// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Product.Application.Responses;
using Product.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Product.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; set; }
        public IFormFile ProductFile { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        //public DateTime CreateAt { get; set; }
        //public DateTime UpdateAt { get; set; }
        public string CategoryId { get; set; }
        public bool IsActive { get; set; }
        //public List<ProductImage> Images { get; set; }
    }
}
