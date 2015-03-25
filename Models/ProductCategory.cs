using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public class ProductCategory
    {
        [JsonProperty("prodcat_id")]
        public long? Id { get; set; }

        [JsonProperty("prodcat_name")]
        public string Name { get; set; }

        [JsonProperty("prodcat_order")]
        public long? Order { get; set; }
    }
}
