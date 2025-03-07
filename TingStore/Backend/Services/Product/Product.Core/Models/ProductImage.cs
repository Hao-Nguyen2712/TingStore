// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Product.Core.Models
{
    public class ProductImage
    {
        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }
        [BsonElement("isPrimary")]
        public bool IsPrimary { get; set; }
        [BsonElement("createAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateAt { get; set; }
    }
}
