using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Stocks.API.ViewModels {

    /// <summary>
    /// View model for account creation requests.
    /// </summary>
    public class CreateAccountViewModel {

        [JsonPropertyName("cash")]
        [JsonProperty("cash")]
        public decimal Cash { get; set; }
    }
}
