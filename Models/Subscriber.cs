using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesAutoPilotAPI.Models
{
    public class Subscriber
    {
        [JsonProperty("id")]
        public decimal? Id { get; set; }

        [JsonProperty("subdate")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? SubscriptionDate { get; set; }

        [JsonProperty("active")]
        [JsonConverter(typeof(BoolConverter))]
        public bool? Status { get; set; }

        [JsonProperty("firstupdate")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? FirstUpdate { get; set; }

        [JsonProperty("lastupdate")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? LastUpdate { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mssys_cookie")]
        public string Cookie { get; set; }

        [JsonProperty("mssys_secure_id")]
        public string SecureId { get; set; }

        [JsonProperty("mssys_last_activity_date")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? LastActivity { get; set; }

        [JsonProperty("mssys_referer_page")]
        public string RefererPage { get; set; }

        [JsonProperty("mssys_useragent")]
        public string UserAgent { get; set; }

        [JsonProperty("mssys_nsid")]
        public long? SignupFormId { get; set; }

        [JsonProperty("mssys_sub_status")]
        [JsonConverter(typeof(BoolConverter))]
        public bool? SignupStatus { get; set; }

        [JsonProperty("mssys_lastname")]
        public string Lastname { get; set; }

        [JsonProperty("mssys_firstname")]
        public string Firstname { get; set; }
        
        [JsonProperty("mssys_fullname")]
        public string Fullname { get; set; }
        
        [JsonProperty("mssys_mobile")]
        public string Mobile { get; set; }

        [JsonProperty("mssys_sendsms_denied")]
        [JsonConverter(typeof(BoolConverter))]
        public bool SMSDenied { get; set; }
       
        [JsonProperty("mssys_int_mobile")]
        public string InternationalMobile { get; set; }

        [JsonProperty("mssys_intm_status")]
        public string InternationalMobileStatus { get; set; }

        [JsonProperty("mssys_phone")]
        public string PhoneNumber { get; set; }

        [JsonProperty("mssys_fax")]
        public string FaxNumber { get; set; }

        [JsonProperty("mssys_company")]
        public string Company { get; set; }

        [JsonProperty("mssys_title")]
        public string Title { get; set; }
        
        [JsonProperty("mssys_vat_number")]
        public string VATNumber { get; set; }

        [JsonProperty("mssys_website")]
        public string WebAddress { get; set; }        
        
        [JsonProperty("mssys_skype")]
        public string Skype { get; set; }        

        [JsonProperty("mssys_photo")]
        public string Photo { get; set; }        
        
        [JsonProperty("mssys_bcard_photo")]
        public string BusinessCard { get; set; }        
        
        [JsonProperty("mssys_nameday")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? Nameday { get; set; }

        [JsonProperty("mssys_birthday")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? Birthday { get; set; }

        [JsonProperty("mssys_gender")]
        public string Gander { get; set; }

        [JsonProperty("mssys_password")]
        public string AccountPassword { get; set; }       
        
        [JsonProperty("mssys_client_ip")]
        public string ClintIP { get; set; }       

        [JsonProperty("mssys_utm_source")]
        public string UTMSource{ get; set; }       

        [JsonProperty("mssys_utm_campaign")]
        public string UTMCampaign{ get; set; }       
        
        [JsonProperty("mssys_utm_medium")]
        public string UTMMedium{ get; set; }       
        
        [JsonProperty("mssys_utm_content")]
        public string UTMContent{ get; set; }       
        
        [JsonProperty("mssys_utm_term")]
        public string UTMTerm{ get; set; }       

        [JsonProperty("mssys_bill_company")]
        public string BillCompany{ get; set; }       
        
        [JsonProperty("mssys_eu_tax")]
        public string EUVatNumber{ get; set; }
        
        [JsonProperty("mssys_bill_country")]
        public string BillCountry{ get; set; }       

        [JsonProperty("mssys_bill_state")]
        public string BillState{ get; set; }       
        
        [JsonProperty("mssys_bill_zip")]
        public string BillZIP{ get; set; }       
                
        [JsonProperty("mssys_bill_city")]
        public string BillCity{ get; set; }            
        
        [JsonProperty("mssys_bill_address")]
        public string BillAddress{ get; set; }            
        
        [JsonProperty("mssys_postal_company")]
        public string PostalCompany{ get; set; }       
        
        [JsonProperty("mssys_postal_country")]
        public string PostalCountry{ get; set; }       

        [JsonProperty("mssys_postal_state")]
        public string PostalState{ get; set; }       
        
        [JsonProperty("mssys_postal_zip")]
        public string PostalZIP{ get; set; }       
                
        [JsonProperty("mssys_postal_city")]
        public string PostalCity{ get; set; }            
        
        [JsonProperty("mssys_postal_address")]
        public string PostalAddress{ get; set; }            

        [JsonProperty("mssys_crm_userid")]
        public long? CRMUser{ get; set; }            
        
        [JsonProperty("mssys_opportunity_value")]
        public long? ValueOfOpportunity{ get; set; }            
        
        [JsonProperty("mssys_longitude")]
        public double? Longitude{ get; set; }            
        
        [JsonProperty("mssys_latitude")]
        public double? Latitude{ get; set; }            

        [JsonProperty("mssys_country_code")]
        public string LocationCountryCode{ get; set; }            

        [JsonProperty("mssys_country")]
        public string LocationCountry{ get; set; }            

        [JsonProperty("mssys_state_code")]
        public string LocationStateCode{ get; set; }            
        
        [JsonProperty("mssys_state")]
        public string LocationState{ get; set; }            

        [JsonProperty("mssys_city")]
        public string LocationCity{ get; set; }            

        [JsonProperty("mssys_crm_status")]
        public string CRMStatus{ get; set; }            

        [JsonProperty("mssys_lastmassage_sent")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? LastCRMMessageSent { get; set; }

        [JsonProperty("mssys_last_statuschange_date")]
        [JsonConverter(typeof(IsoDateTimeConverterWithNull))]
        public DateTime? LastStatusChangeDate { get; set; }

        [JsonProperty("mssys_net_referral")]
        public string CRMReferralPerson{ get; set; }            
        
        [JsonProperty("mssys_net_referred")]
        public string NetReferredPersons{ get; set; }            
        
        [JsonProperty("mssys_net_lookingfor")]
        public string NetLookingFor{ get; set; }            
              
        [JsonProperty("mssys_net_contact_industry")]
        public string NetContactByIndustries{ get; set; }

        [JsonProperty("mssys_net_contact_company")]
        public string ContactForCompanies{ get; set; }
    }
}
    




