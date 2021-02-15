using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Stocks.API.ViewModels {

    /// <summary>
    /// Balance overview after an order is placed
    /// </summary>
    public class OrderResultViewModel : BusinessErrorsViewModel {

        /// <summary>
        /// General information on the account related to an order
        /// </summary>
        [JsonPropertyName("current_balance")]
        [JsonProperty("current_balance")]
        public AccountViewModel AccountOverview {get; set;}

        public OrderResultViewModel() : base() {
        }
    }
}
