using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public class CouponCheck
    {
        [JsonProperty("coupon_code")]
        public string Code { get; set; }

        [JsonProperty("products")]
        public List<Product> OrderedProducts { get; set; }

        [JsonProperty("netto_discount")]
        public double? NetDiscount { get; set; }

        [JsonProperty("brutto_discount")]
        public double? GrossDiscount { get; set; }
    }
}
