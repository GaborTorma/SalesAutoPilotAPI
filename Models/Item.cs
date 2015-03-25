using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public class Item
    {
        [JsonProperty("oi_id")]
        public long? Id { get; set; }

        [JsonProperty("oi_sku")]
        public string SKU { get; set; }

        [JsonProperty("oi_name")]
        public string Name { get; set; }

        [JsonProperty("oi_price")]
        public double? Price { get; set; }

        [JsonProperty("oi_quantity")]
        public long? Quantity { get; set; }

        [JsonProperty("oi_netto_sum")]
        public double? NetPrice { get; set; }

        [JsonProperty("oi_vat_percent")]
        public byte? Vat { get; set; }

        [JsonProperty("oi_vat_name")]
        public string VatName { get; set; }
        
        [JsonProperty("oi_brutto_sum")]
        public double? GrossPrice { get; set; }

        [JsonProperty("oi_currency")]
        public string Currency { get; set; }

        [JsonProperty("nud_id")]
        public long? OrderId { get; set; }
        
        [JsonProperty("prod_id")]
        public long? ProductId { get; set; }
    }
}
