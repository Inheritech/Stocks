using NUnit.Framework;
using Stocks.Domain.Aggregates.TransactionAggregate;
using Stocks.Domain.Exceptions;

namespace Stocks.Domain.Tests.TransactionAggregate {

    [TestFixture]
    public class OperationTests {

        [Test]
        public void FromNameThrowsWhenTheOperationNameIsInvalid() {
            // Arrange
            static void obtain() => Operation.FromName("NonExistent");


            // Act/Assert
            Assert.Throws<InvalidTransactionOperationException>(obtain);
        }

    }
}
