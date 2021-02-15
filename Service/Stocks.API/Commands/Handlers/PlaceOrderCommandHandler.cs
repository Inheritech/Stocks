﻿using MediatR;
using Microsoft.Extensions.Logging;
using Stocks.API.Commands.Results;
using Stocks.Domain.Aggregates.AccountAggregate;
using Stocks.Domain.Aggregates.TransactionAggregate;
using Stocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stocks.API.Commands.Handlers {
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, PlaceOrderResult> {

        private readonly IAccountRepository _accounts;
        private readonly ITransactionRepository _transactions;
        private readonly ILogger<PlaceOrderCommandHandler> _logger;

        public PlaceOrderCommandHandler(
            IAccountRepository accounts, 
            ITransactionRepository transactions,
            ILogger<PlaceOrderCommandHandler> logger
        ) {
            _accounts = accounts;
            _transactions = transactions;
            _logger = logger;
        }

        public async Task<PlaceOrderResult> Handle(PlaceOrderCommand request, CancellationToken cancellationToken) {
            var account = await _accounts.FindAsync(request.AccountId);
            if (account == null)
                throw new AccountNotFoundException();

            try {
                var transaction = account.PlaceOrder(
                    request.Timestamp,
                    Operation.FromName(request.Operation),
                    request.Issuer,
                    request.Shares,
                    request.SharePrice
                );

                var duplicateTransaction = _transactions.FindDuplicateOnTimeSpan(transaction, TimeSpan.FromMinutes(5));
                if (duplicateTransaction != null)
                    throw new DuplicateTransactionException();

                _accounts.Update(account);
                _transactions.Add(transaction);

                await _accounts.UnitOfWork.SaveEntitiesAsync();
                await _transactions.UnitOfWork.SaveEntitiesAsync();
            } catch (DomainException domainException) {
                return CreateResultFromAccount(account, domainException.Code);
            }

            return CreateResultFromAccount(account);
        }

        private static PlaceOrderResult CreateResultFromAccount(Account account, string domainErrorCode = null) {
            var domainErrors = new List<string>();

            if (domainErrorCode != null)
                domainErrors.Add(domainErrorCode);

            return new PlaceOrderResult(
                new PlaceOrderResult.AccountOverview(
                    account.Cash,
                    account.StockBalances
                        .Select(_ => new PlaceOrderResult.AccountOverview.StockBalanceOverview(_.Issuer, _.Shares, _.SharePrice))
                ),
                domainErrors
            );
        }
    }
}