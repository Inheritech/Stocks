using MediatR;
using Stocks.API.Commands.Results;

namespace Stocks.API.Commands {

    public class CreateAccountCommand : IRequest<CreateAccountResult> {

        public decimal InitialCash { get; }

        public CreateAccountCommand(decimal cash) {
            InitialCash = cash;
        }

    }
}
