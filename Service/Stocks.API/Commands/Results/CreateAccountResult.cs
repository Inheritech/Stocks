namespace Stocks.API.Commands.Results {
    public class CreateAccountResult {

        public int Id { get; }
        public decimal Cash { get; }

        public CreateAccountResult(int id, decimal cash) {
            Id = id;
            Cash = cash;
        }

    }
}
