using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Stocks.API.ViewModels {

    /// <summary>
    /// Information about a stock balance
    /// </summary>
    public class StockBalanceViewModel {

        /// <summary>
        /// Ticker of the stock for this balance.
        /// </summary>
        [JsonPropertyName("issuer_name")]
        [JsonProperty("issuer_name")]
        public string Issuer { get; set; }

        /// <summary>
        /// Amount of shares in this balance.
        /// </summary>
        [JsonPropertyName("total_shares")]
        [JsonProperty("total_shares")]
        public int TotalShares { get; set; }

        /// <summary>
        /// Average price of the stocks on balance.
        /// </summary>
        [JsonPropertyName("share_price")]
        [JsonProperty("share_price")]
        public decimal SharePrice { get; set; }
    }
}
