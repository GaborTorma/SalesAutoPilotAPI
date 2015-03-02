using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public class MessageParamter
    {
        [JsonProperty("send_id")]
        public long SendId { get; set; }

        [JsonProperty("contactid")]
        public long? ContactId { get; set; }

        [JsonProperty("mssys_text_content_part")]
        public string TextContent { get; set; }

        [JsonProperty("mssys_html_content_part")]
        public string HTMLContent { get; set; }

        public MessageParamter(long sendid, long? contactid, string textcontent = null, string htmlcontent = null)
        {
            SendId = sendid;
            ContactId = contactid;
            TextContent = textcontent;
            HTMLContent = htmlcontent;
        }
    }
}
