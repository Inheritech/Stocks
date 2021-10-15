using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stocks.API.Commands;
using Stocks.API.Commands.Results;
using Stocks.API.ViewModels;
using Stocks.Domain.Exceptions;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Stocks.API.Controllers {
    [Route("/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase {

        private readonly IMediator _mediator;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IMediator mediator, ILogger<AccountsController> logger) {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Create a new account from the provided information.
        /// </summary>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AccountViewModel>> CreateAccountAsync([FromBody] CreateAccountRequest viewModel) {
            _logger.LogDebug(
                "Processing request received at endpoint {EndpointName}",
                nameof(CreateAccountAsync)
            );
            var createAccountCommand = new CreateAccountCommand(viewModel.Cash);
            try {
                var createdAccountResult = await _mediator.Send(createAccountCommand);
                var accountVM = new IdentifiedAccountViewModel(createdAccountResult.Id, createdAccountResult.Cash);
                return Ok(accountVM);
            } catch (DomainException domainException) {
                var errorModel = new BusinessErrorsViewModel();
                errorModel.BusinessErrors.Add(domainException.Code);
                return BadRequest(errorModel);
            } catch (Exception internalError) {
                _logger.LogError(internalError, "An unexpected error occured while processing the following request: {ViewModel}", viewModel);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Place an order in the system
        /// </summary>
        [HttpPost("{id}/orders")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OrderResultViewModel>> PlaceOrderAsync([FromRoute] int id, [FromBody] PlaceOrderRequest viewModel) {
            _logger.LogDebug(
                "Processing request received at endpoint {EndpointName}",
                nameof(PlaceOrderAsync)
            );
            var orderTime = DateTimeOffset.FromUnixTimeSeconds(viewModel.Timestamp).DateTime;
            var placeOrderCommand = new PlaceOrderCommand(
                id, 
                orderTime, 
                viewModel.Operation, 
                viewModel.Issuer, 
                viewModel.TotalShares, 
                viewModel.SharePrice
            );
            try {
                var placedOrderResult = await _mediator.Send(placeOrderCommand);
                var placedOrderVM = MapOrderCommandResultToVM(placedOrderResult);
                if (placedOrderResult.IsSuccess())
                    return Ok(placedOrderVM);
                else
                    return BadRequest(placedOrderVM);
            } catch (DomainException domainException) {
                var errorModel = new BusinessErrorsViewModel();
                errorModel.BusinessErrors.Add(domainException.Code);
                return BadRequest(errorModel);
            } catch (Exception internalError) {
                _logger.LogError(internalError, "An unexpected error occured while processing the following request: {ViewModel}", viewModel);
                return StatusCode(500);
            }
        }

        private OrderResultViewModel MapOrderCommandResultToVM(PlaceOrderResult result) {
            var placedOrderVM = new OrderResultViewModel {
                AccountOverview = new AccountViewModel(result.Account.Cash) {
                    StockBalances = result.Account
                        .StockBalances.Select(_ => new StockBalanceViewModel(_.Issuer, _.Shares, _.SharePrice)).ToList()
                },
                BusinessErrors = result.BusinessErrors.ToList()
            };

            return placedOrderVM;
        }
    }
}
