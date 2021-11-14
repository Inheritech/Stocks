using System.Text.Json.Serialization;

namespace Stocks.API.ViewModels {

    /// <summary>
    /// Request data for creating an account
    /// </summary>
    public class CreateAccountRequest {

        [JsonPropertyName("cash")]
        public decimal Cash { get; set; }
    }
}
