using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public class ModifyData<T> where T : Subscriber
    {
        [JsonProperty("id_field")]
        public string Field { get; set; }

        [JsonProperty("id_value")]
        public string Value { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
