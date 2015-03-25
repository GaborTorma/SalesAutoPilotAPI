using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public enum OrderStatus
    {
        Ordered = 1,
        Payed,
        PayedBack,
        IncompleteOrder
    }

    public enum PaymentMenthod
    {
        Wire = 1,
        Cash,
        CashOnDelivery,
        CreditCard,
        RecurringPayment
    }

	public enum ShippingMethod
	{ 
		Personal,
		Shipping,
		Online
	}

	public class Order : Subscriber
    {
        [JsonProperty("mssys_contact_id")]
        public long? ContactId { get; set; }

        [JsonProperty("mssys_order_status")]
        public OrderStatus? OrderStatus { get; set; }

        // Payment

        [JsonProperty("mssys_order_netto_sum")]
        public double? NetValue { get; set; }

        [JsonProperty("mssys_order_brutto_sum")]
        public double? GrossValue { get; set; }

        [JsonProperty("mssys_payment_method")]
        public PaymentMenthod? PaymentMenthod { get; set; }

        [JsonProperty("mssys_online_payment_method")]
        public string OnlinePaymentMethod { get; set; }

        [JsonProperty("mssys_bill_currency")]
        public string CurrencyCode { get; set; }

        [JsonProperty("mssys_execute_date")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? ExecuteDate { get; set; }

        [JsonProperty("mssys_pay_due_date")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? PayDueDate { get; set; }

        [JsonProperty("mssys_pay_date")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? PayDate { get; set; }

        // Shipping

		[JsonProperty("mssys_shipping_modetype")]
        public long? ShippingModeId { get; set; }

        [JsonProperty("mssys_shipping_mode")]
        public string ShippingModeName { get; set; }

        [JsonProperty("mssys_netto_shipping_cost")]
        public double? NetShippingCost { get; set; }

        [JsonProperty("mssys_shipping_cost")]
        public double? GrossShippingCost { get; set; }

        // Invoicing

        [JsonProperty("mssys_proform_number")]
        public string ProformNumber { get; set; }

        [JsonProperty("mssys_proform_pdf_link")]
        public string ProformPDFLink { get; set; }

        [JsonProperty("mssys_billing_number")]
        public string BillNumber { get; set; }

        [JsonProperty("mssys_bill_pdf_link")]
        public string BillPDFLink { get; set; }
        
        [JsonProperty("mssys_bill_comment")]
        public string CommentOnBill { get; set; }

        [JsonProperty("mssys_billing_system_syncdate")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? SyncDateWithBillingSystem { get; set; }

        [JsonProperty("mssys_trnum")]
        public string BankTransactionNumber { get; set; }

        [JsonProperty("mssys_anum")]
        public string BankTransactionControlNumber { get; set; }

        [JsonProperty("mssys_rt")]
        public string BankMessage { get; set; }

        // Coupons

        [JsonProperty("mssys_coupon_type")]
        public long? CouponType { get; set; }

        [JsonProperty("mssys_coupon")]
        public string CouponCode { get; set; }

        [JsonProperty("mssys_coupon_id")]
        public long? CouponId { get; set; }

        [JsonProperty("mssys_coupon_discount_amount_netto")]
        public double? NetCouponDiscount { get; set; }

        [JsonProperty("mssys_coupon_discount_amount_brutto")]
        public double? GrossCouponDiscount { get; set; }


        [JsonProperty("mssys_shoprenter_id")]
        public long? WebshopId { get; set; }

        // Products

        [JsonProperty("mssys_ordered_items")]
        public string OrderedItems { get; set; }

        [JsonProperty("products")]
        public List<Item> Items { get; set; }

        public Order()
        {
            Items = new List<Item>();
        }

        // Read-only

        public long? shipping_method
        { 
            get 
            { 
                return ShippingModeId; 
            } 
        }

		public long? prod_id
        { 
            get 
            {
                if (Items == null)
                    return null;
                return Items.Count == 0 ? null : Items[0].ProductId; 
            }
        }
    }
}
