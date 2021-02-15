using Stocks.Domain.Common;
using Stocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.Domain.Aggregates.TransactionAggregate {

    /// <summary>
    /// Represents the operation kind of a given transaction.
    /// </summary>
    public class Operation : Enumeration {

        public static Operation Buy = new Operation(1, nameof(Buy));
        public static Operation Sell = new Operation(2, nameof(Sell));

        protected Operation(int id, string name) : base(id, name) {
        }

        public static IEnumerable<Operation> List() =>
            new[] { Buy, Sell };

        /// <summary>
        /// Obtain an operation type based on its name.
        /// </summary>
        /// <param name="name">Name of the operation</param>
        /// <exception cref="InvalidTransactionOperationValueException">Thrown if the name provided is not a valid operation type.</exception>
        public static Operation FromName(string name) {
            var operation = List()
                .SingleOrDefault(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (operation == null)
                throw new InvalidTransactionOperationValueException(name);

            return operation;
        }
    }
}
