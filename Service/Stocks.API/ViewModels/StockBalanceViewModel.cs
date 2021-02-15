using Newtonsoft.Json;

namespace Stocks.API.ViewModels {

    /// <summary>
    /// Information about a stock balance
    /// </summary>
    public class StockBalanceViewModel {

        /// <summary>
        /// Ticker of the stock for this balance.
        /// </summary>
        [JsonProperty("issuer_name")]
        public string Issuer { get; set; }

        /// <summary>
        /// Amount of shares in this balance.
        /// </summary>
        [JsonProperty("total_shares")]
        public int TotalShares { get; set; }

        /// <summary>
        /// Average price of the stocks on balance.
        /// </summary>
        [JsonProperty("share_price")]
        public decimal SharePrice { get; set; }
    }
}
