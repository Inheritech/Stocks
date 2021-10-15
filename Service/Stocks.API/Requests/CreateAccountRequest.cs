using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Stocks.API.ViewModels {

    /// <summary>
    /// Request data for creating an account
    /// </summary>
    public class CreateAccountRequest {

        [JsonPropertyName("cash")]
        [JsonProperty("cash")]
        public decimal Cash { get; set; }
    }
}
