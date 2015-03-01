using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public class Product
    {
        [JsonProperty("prod_id")]
        public long? Id { get; set; }

        [JsonProperty("prod_name")]
        public string Name { get; set; }

        [JsonProperty("prod_price")]
        public double? Price { get; set; }

        [JsonProperty("prod_vat_percent")]
        public byte? Vat { get; set; }

        [JsonProperty("prod_currency")]
        public string Currency { get; set; }

        [JsonProperty("prod_sku")]
        public string SKU { get; set; }

        [JsonProperty("prodcat_id")]
        public long? CategoryId
        {
            get
            {
                return CategoryIds.Count == 0 ? null : CategoryIds[0];
            }
            set
            {
                CategoryIds.Clear();
                CategoryIds.Add(value);
            }
        }

        [JsonProperty("prodcat_ids")]
        public List<long?> CategoryIds { get; set; }

        [JsonProperty("prodcat_name")]
        public string CategoryName { get; set; }

        [JsonProperty("qty")]
        public long? Quantity { get; set; }

        [JsonProperty("prod_deleted")]
        [JsonConverter(typeof(BoolConverter))]
        public bool? Deleted { get; set; }

        public Product()
        {
            CategoryIds = new List<long?>();
        }
    }
}
