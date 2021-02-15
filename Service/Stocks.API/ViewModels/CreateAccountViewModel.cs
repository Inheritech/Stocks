using Newtonsoft.Json;

namespace Stocks.API.ViewModels {

    /// <summary>
    /// View model for account creation requests.
    /// </summary>
    public class CreateAccountViewModel {

        [JsonProperty("cash")]
        public decimal Cash { get; set; }
    }
}
