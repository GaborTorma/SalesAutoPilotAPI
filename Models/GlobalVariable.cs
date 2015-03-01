using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public class GlobalVariable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("html")]
        public string HTML { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
