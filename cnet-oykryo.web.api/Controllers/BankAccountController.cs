using cnet_oykryo.application.Services;
using cnet_oykryo.web.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="customerService"></param>
        /// <param name="logger"></param>
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

        /// <summary>
        /// Transfer amounts between any two accounts, including those owned by different customers
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("transfer")]
        public async Task<IActionResult> TransferAmount([FromBody] TransferAmountRequest request)
        {
            try
            {
                // Validate the request
                if (request == null)
                {
                    _logger.LogWarning("Invalid transfer request.");
                    return BadRequest("Invalid transfer request");
                }

                // Validate the source and destination accounts
                var sourceAccount = await _accountService.GetAccountByAccountNumber(request.FromAccountNumber);
                var destinationAccount = await _accountService.GetAccountByAccountNumber(request.ToAccountNumber);

                if (sourceAccount == null || destinationAccount == null)
                {
                    _logger.LogWarning("One or both accounts not found.");
                    return NotFound("One or both accounts not found");
                }

                // Validate if the source account has sufficient balance
                if (sourceAccount.Balance < request.Amount)
                {
                    _logger.LogWarning("Insufficient balance in the source account.");

                    return BadRequest("Insufficient balance in the source account");
                }

                // Perform the fund transfer
               await _accountService.TransferAmount(sourceAccount, destinationAccount, request.Amount);

                // Optionally, log the transfer or perform other actions
                _logger.LogInformation("Transfer successful.");  // Log information

                return Ok("Transfer successful");
            }
            catch (Exception ex)
            {
                // Log the exception, handle errors, and return an appropriate response
                _logger.LogError(ex, "Error during fund transfer");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get balance for a specific account
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [HttpGet("{accountNumber}/balance")]
        public async Task<IActionResult> GetAccountBalance(string accountNumber)
        {
            try
            {
                // Validate the account ID
                if (accountNumber == "")
                {
                    _logger.LogWarning("Invalid account ID.");
                    return BadRequest("Invalid account ID");
                }

                // Retrieve the account
                var account = await _accountService.GetAccountByAccountNumber(accountNumber);

                // Check if the account exists
                if (account == null)
                {
                    _logger.LogWarning("Account not found.");

                    return NotFound("Account not found");
                }

                _logger.LogInformation("retrieving account balance successful.");  // Log information

                // Return the account balance
                return Ok(new { AccountId = account.Id, Balance = account.Balance });
            }
            catch (Exception ex)
            {
                // Log the exception, handle errors, and return an appropriate response
                _logger.LogError(ex, "Error retrieving account balance");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Retrieve transfer history for a given account.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [HttpGet("{accountNumber}/transfer-history")]
        public async Task<IActionResult> GetTransferHistory(string accountNumber)
        {
            try
            {
                // Validate the account ID
                if (accountNumber == "")
                {
                    _logger.LogWarning("Invalid account ID provided: {AccountId}", accountNumber);
                    return BadRequest("Invalid account ID");
                }

                // Retrieve the account
                var account = await _accountService.GetAccountByAccountNumber(accountNumber);

                // Check if the account exists
                if (account == null)
                {
                    _logger.LogWarning("Account not found for ID: {AccountId}", accountNumber);
                    return NotFound("Account not found");
                }

                // Retrieve the transfer history for the account
                var transferHistory = await _accountService.GetTransferHistory(account);

                // Log successful result
                _logger.LogInformation("Transfer history retrieved successfully for Account ID: {AccountId}", accountNumber);

                // Return the transfer history
                return Ok(transferHistory.Select(x => new { Amount = x.Amount, TransferDate = x.TransferDate, SourceAccount = x.SourceAccountId }).ToList());
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error retrieving transfer history for Account ID: {AccountId}", accountNumber);

                // Handle errors and return an appropriate response
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
