using MediatR;
using Stocks.API.Commands.Results;
using Stocks.Domain.Aggregates.AccountAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stocks.API.Commands.Handlers {

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResult> {

        private readonly IAccountRepository _accounts;

        public CreateAccountCommandHandler(IAccountRepository accounts) {
            _accounts = accounts;
        }

        public async Task<CreateAccountResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken) {
            var account = new Account(request.InitialCash);
            _accounts.Add(account);
            if (!await _accounts.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                throw new Exception("Could not save entities.");

            return new CreateAccountResult(account.Id, account.Cash);
        }
    }
}
