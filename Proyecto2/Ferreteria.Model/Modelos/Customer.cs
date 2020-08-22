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
    public class Customer
    {

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("age")]
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int Age { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("satisfaction")]
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int Satisfaction { get; set; }

        [BsonExtraElements]
        public BsonDocument Metadata { get; set; }

    }
}
