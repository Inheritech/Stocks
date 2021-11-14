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
        public string Issuer { get; set; }

        /// <summary>
        /// Amount of shares in this balance.
        /// </summary>
        [JsonPropertyName("total_shares")]
        public int TotalShares { get; set; }

        /// <summary>
        /// Average price of the stocks on balance.
        /// </summary>
        [JsonPropertyName("share_price")]
        public decimal SharePrice { get; set; }

        public StockBalanceViewModel(string issuer, int totalShares, decimal sharePrice) {
            Issuer = issuer;
            TotalShares = totalShares;
            SharePrice = sharePrice;
        }
    }
}
