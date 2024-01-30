using cnet_oykryo.application.Services;
using cnet_oykryo.web.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace cnet_oykryo.web.api.Controllers
{
    /// <summary>
    /// Represents a controller for managing accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly ILogger<BankAccountController> _logger;  // Inject the logger


        public BankAccountController(IAccountService accountService, ICustomerService customerService, ILogger<BankAccountController> logger)
        {
            _accountService = accountService;
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The result of the account creation operation.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            try
            {
                // Validate the request
                if (request == null)
                {
                    return BadRequest("Invalid request body");
                }

                // Call the method from _accountService to create an account
                var customer = await _customerService.GetCustomerByIdAsync(request.CustomerId);

                if (customer == null)
                {
                    _logger.LogWarning("Customer not found.");
                    // Customer not found
                    return NotFound("Customer not found");
                }

                await _accountService.CreateAccount(customer, request.InitialDeposit);

                _logger.LogInformation("Account created successfully.");  // Log information


                // Return a successful response
                return Ok("Account created successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error creating account.");  // Log error

                // You might want to handle specific exceptions differently based on your application's needs
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("transfer")]
        public IActionResult TransferAmount([FromBody] TransferAmountRequest request)
        {
            // Validate request and perform the fund transfer
            _accountService.TransferAmount(new domain.Entities.BankAccount { AccountNumber = request.FromAccountNumber },
                new domain.Entities.BankAccount { AccountNumber = request.ToAccountNumber }, request.Amount);

            // Return success
            return Ok("Transfer successful");
        }

        [HttpGet("{accountNumber}/balance")]
        public IActionResult GetAccountBalance(string accountNumber)
        {
            // Get account balance
            var balance = _accountService.GetAccountBalance(new domain.Entities.BankAccount { AccountNumber = accountNumber });

            // Return the balance
            return Ok(balance);
        }

        [HttpGet("{accountNumber}/transfer-history")]
        public IActionResult GetTransferHistory(string accountNumber)
        {
            // Get transfer history
            var transferHistory = _accountService.GetTransferHistory(new domain.Entities.BankAccount { AccountNumber = accountNumber });

            // Return the transfer history
            return Ok(transferHistory);
        }
    }
}
