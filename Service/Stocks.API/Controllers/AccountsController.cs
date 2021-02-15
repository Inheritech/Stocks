using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stocks.API.Commands;
using Stocks.API.ViewModels;
using Stocks.Domain.Exceptions;
using System;
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
        public async Task<ActionResult<AccountViewModel>> CreateAccountAsync([FromBody] CreateAccountViewModel viewModel) {
            _logger.LogDebug(
                "Processing received request at endpoint {EndpointName}",
                nameof(CreateAccountAsync)
            );
            var createAccountCommand = new CreateAccountCommand(viewModel.Cash);
            try {
                var createdAccountResult = await _mediator.Send(createAccountCommand);
                var accountVM = new AccountViewModel(createdAccountResult.Id, createdAccountResult.Cash);
                return Ok(accountVM);
            } catch (DomainException domainException) {
                var errorModel = new ErrorViewModel();
                errorModel.BusinessErrors.Add(domainException.Code);
                return BadRequest(errorModel);
            } catch (Exception internalError) {
                _logger.LogError(internalError, "An unexpected error occured while processing the following request: {ViewModel}", viewModel);
                return StatusCode(500);
            }
        }

    }
}
