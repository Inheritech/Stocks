using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stocks.API.ViewModels {

    public class AccountViewModel {

        /// <summary>
        /// Current cash balance of the account
        /// </summary>
        [JsonPropertyName("cash")]
        [JsonProperty("cash")]
        public decimal Cash { get; set; }

        /// <summary>
        /// Current stock balances for this account
        /// </summary>
        [JsonPropertyName("issuers")]
        [JsonProperty("issuers")]
        public List<StockBalanceViewModel> StockBalances { get; set; }

        public AccountViewModel() {
            StockBalances = new List<StockBalanceViewModel>();
        }

        public AccountViewModel(decimal cash) : this() {
            Cash = cash;
        }
    }

    public class IdentifiedAccountViewModel : AccountViewModel {
        /// <summary>
        /// Identifier of the account
        /// </summary>
        [JsonPropertyName("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        public IdentifiedAccountViewModel(int id, decimal cash) : base(cash) {
            Id = id;
        }
    }
}
