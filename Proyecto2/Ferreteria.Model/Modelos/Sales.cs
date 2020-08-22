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
    public class Sales
    {
        [BsonId]
        public ObjectId SalesID { get; set; }

        [BsonElement("saleDate")]
        public DateTime SalesDate { get; set; }

        [BsonElement("items")]
        public IList<LItem> Items { get; set; }

        [BsonElement("storeLocation")]
        public string Location { get; set; }

        [BsonElement("customer")]
        public Customer Customer { get; set; }

        [BsonElement("couponUse")]
        public bool CouponUse { get; set; }

        [BsonElement("purchaseMethod")]
        public string PurchaseMethod { get; set; }

        [BsonExtraElements]
        public BsonDocument Metadata { get; set; }


    }
}
