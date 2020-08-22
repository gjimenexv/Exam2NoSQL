using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Model.Modelos
{
    [BsonIgnoreExtraElements]
    public class LItem
    {
        //: Cannot deserialize a 'String' from BsonType 'Decimal128'.
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; }

        [BsonElement("price")]
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public double Price { get; set; }

        [BsonElement("quantity")]
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int Quantity  { get; set; }

        [BsonExtraElements]
        public BsonDocument Metadata { get; set; }

    }
}
