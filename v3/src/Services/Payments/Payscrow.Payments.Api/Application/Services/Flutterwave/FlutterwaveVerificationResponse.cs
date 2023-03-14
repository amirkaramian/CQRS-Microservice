using Newtonsoft.Json;
using System;

namespace Payscrow.Payments.Api.Application.Services.Flutterwave
{
    public static class FlutterwaveVerification
    {  

        public class Response
        {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public class Data
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("tx_ref")]
            public string TxRef { get; set; }

            [JsonProperty("flw_ref")]
            public string FlwRef { get; set; }

            [JsonProperty("device_fingerprint")]
            public string DeviceFingerprint { get; set; }

            [JsonProperty("amount")]
            public decimal Amount { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("charged_amount")]
            public long ChargedAmount { get; set; }

            [JsonProperty("app_fee")]
            public long AppFee { get; set; }

            [JsonProperty("merchant_fee")]
            public long MerchantFee { get; set; }

            [JsonProperty("processor_response")]
            public string ProcessorResponse { get; set; }

            [JsonProperty("auth_model")]
            public string AuthModel { get; set; }

            [JsonProperty("ip")]
            public string Ip { get; set; }

            [JsonProperty("narration")]
            public string Narration { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("payment_type")]
            public string PaymentType { get; set; }

            [JsonProperty("created_at")]
            public DateTimeOffset CreatedAt { get; set; }

            [JsonProperty("account_id")]
            public long AccountId { get; set; }

            [JsonProperty("amount_settled")]
            public long AmountSettled { get; set; }

            [JsonProperty("card")]
            public Card Card { get; set; }

            [JsonProperty("customer")]
            public Customer Customer { get; set; }
        }

        public class Card
        {
            [JsonProperty("first_6digits")]
            //[JsonConverter(typeof(ParseStringConverter))]
            public long First6Digits { get; set; }

            [JsonProperty("last_4digits")]
            //[JsonConverter(typeof(ParseStringConverter))]
            public long Last4Digits { get; set; }

            [JsonProperty("issuer")]
            public string Issuer { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("expiry")]
            public string Expiry { get; set; }
        }

        public class Customer
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("phone_number")]
            public string PhoneNumber { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("created_at")]
            public DateTimeOffset CreatedAt { get; set; }
        }
   
  }

}
