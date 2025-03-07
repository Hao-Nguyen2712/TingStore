using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Product.Core.Models;

namespace Product.Application.Responses
{
	public class ProductResponse
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("productImage")]
        public string ProductImage { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }
        [BsonElement("stock")]
        public int Stock { get; set; }
        [BsonElement("createAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateAt { get; set; }
        [BsonElement("updateAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdateAt { get; set; }
        [BsonElement("categoryId")]
        public string CategoryId { get; set; }
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
        [BsonElement("images")]
        public List<ProductImage> Images { get; set; }
    }
}
