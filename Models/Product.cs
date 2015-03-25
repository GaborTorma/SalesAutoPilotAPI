using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public class Product
    {
        [JsonProperty("prod_id")]
        public long? Id { get; set; }

        [JsonProperty("prod_sku")]
        public string SKU { get; set; }

        [JsonProperty("prod_name")]
        public string Name { get; set; }

        [JsonProperty("prod_price")]
        public double? Price { get; set; }

        [JsonProperty("prod_vat_percent")]
        public byte? Vat { get; set; }

        [JsonProperty("prod_currency")]
        public string Currency { get; set; }

        [JsonProperty("prodcat_id")]
        public long? CategoryId
        {
            get
            {
                return _CategoryIds.Count == 0 ? null : _CategoryIds[0];
            }
            set
            {
                _CategoryIds.Clear();
                _CategoryIds.Add(value);
            }
        }

        private List<long?> _CategoryIds;

        [JsonProperty("prodcat_ids")]  
        public List<long?> CategoryIds
        {
            get
            {
                return _CategoryIds;
            }
            set
            {
                _CategoryIds = value;
            }
        }

		public List<ProductCategory> prodcats // only for read api values
		{
            set
            {
				CategoryIds.Clear();
				foreach (ProductCategory ProductCategory in value)
					CategoryIds.Add(ProductCategory.Id);

            }
		}
		
        [JsonProperty("qty")]  // not valid for Product
        public long? Quantity { get; set; }

        public Product()
        {
			_CategoryIds = new List<long?>();
        }
    }
}
