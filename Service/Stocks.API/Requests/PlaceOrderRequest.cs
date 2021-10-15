using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Stocks.API.ViewModels {

    /// <summary>
    /// Request data for placing a stocks order
    /// </summary>
    public class PlaceOrderRequest {

        /// <summary>
        /// Epoch at which the order was placed
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Operation to make with this order
        /// </summary>
        [JsonPropertyName("operation")]
        [JsonProperty("operation")]
        public string Operation { get; set; }
  
        /// <summary>
        /// Name of the share issuer
        /// </summary>
        [JsonPropertyName("issuer_name")]
        [JsonProperty("issuer_name")]
        public string Issuer { get; set; }

        /// <summary>
        /// Total amount of shares
        /// </summary>
        [JsonPropertyName("total_shares")]
        [JsonProperty("total_shares")]
        public int TotalShares { get; set; }

        /// <summary>
        /// Price of each share
        /// </summary>
        [JsonPropertyName("share_price")]
        [JsonProperty("share_price")]
        public decimal SharePrice { get; set; }
    }
}
