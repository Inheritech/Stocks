using MediatR;
using Stocks.API.Commands.Results;
using System;

namespace Stocks.API.Commands {
    public class PlaceOrderCommand : IRequest<PlaceOrderResult> {
        public int AccountId { get; }
        public DateTime Timestamp { get; }
        public string Operation { get; }
        public string Issuer { get; }
        public int Shares { get; }
        public decimal SharePrice { get; }

        public PlaceOrderCommand(int accountId, DateTime timestamp, string operation, string issuer, int shares, decimal sharePrice) {
            AccountId = accountId;
            Timestamp = timestamp;
            Operation = operation;
            Issuer = issuer;
            Shares = shares;
            SharePrice = sharePrice;
        }
    }
}
