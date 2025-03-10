using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Product.Core.Models
{
    public class Review
    {
        [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;} = ObjectId.GenerateNewId().ToString();

        [BsonElement("productId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [BsonElement("customerId")]
        public string CustomerId { get; set; }

        [BsonElement("rating")]
        public int Rating { get; set; }

        [BsonElement("comment")]
        public string? Comment  { get; set; }

        [BsonElement("createAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateAt {get; set;}

        [BsonElement("updateAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdateAt {get; set;}
    }
}
