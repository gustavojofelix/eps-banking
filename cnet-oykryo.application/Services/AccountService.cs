using cnet_oykryo.domain.Entities;
using cnet_oykryo.domain.Services;
using cnet_oykryo.Infrastructure.Repositories;

namespace cnet_oykryo.application.Services
{
    // AccountService: Handles operations related to bank accounts
    public class AccountService : IAccountService
    {
        private readonly IBankAccountRepository _accountRepository;
        private readonly ITransferRepository _transferRepository;

        public AccountService(IBankAccountRepository accountRepository, ITransferRepository transferRepository)
        {
            _accountRepository = accountRepository;
            _transferRepository = transferRepository;
        }

        public async Task CreateAccount(Customer customer, decimal initialDeposit)
        {
            // Validate inputs
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            }

            if (initialDeposit < 0)
            {
                throw new ArgumentException("Initial deposit must be a non-negative amount.", nameof(initialDeposit));
            }

            // Create a new bank account
            var newAccount = new BankAccount
            {
                CustomerId = customer.Id,
                Balance = initialDeposit
                // Additional properties can be set here
            };

            // Add the new account to the customer's list of accounts
            customer.BankAccounts.Add(newAccount);



            newAccount.AccountNumber = GenerateAccountNumber();

            // Example: Triggering a domain event
            // DomainEvents.Raise(new AccountCreatedEvent(newAccount));

            // Use the repository to add the new account to the database
            try
            {
                await _accountRepository.AddBankAccountAsync(newAccount);
            }
            catch (Exception ex)
            {
                // Log the exception or take appropriate actions
                // Example: logger.LogError(ex, "Error adding bank account to the database.");
                throw; // Re-throw the exception to maintain the exception chain
            }

        }

        public async Task TransferAmount(BankAccount sourceAccount, BankAccount destinationAccount, decimal amount)
        {// Business logic for transferring funds between accounts

            // Validate inputs
            ValidateTransferInputs(sourceAccount, destinationAccount, amount);

            // Check if the source account has sufficient balance
            if (sourceAccount.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds in the source account.");
            }

            // Deduct amount from the source account
            sourceAccount.Balance -= amount;

            // Add amount to the destination account
            destinationAccount.Balance += amount;

            // Record the transfer in the source and destination accounts
            var transfer = new Transfer
            {
                SourceAccountId = sourceAccount.Id,
                DestinationAccountId = destinationAccount.Id,
                Amount = amount,
                TransferDate = DateTime.UtcNow
                // Additional properties can be set here
            };

            sourceAccount.Transfers.Add(transfer);
            destinationAccount.Transfers.Add(transfer);

            // Optionally,   perform other actions like triggering domain events
            // or persisting changes to the database.

            // Example: Triggering a domain event
            // DomainEvents.Raise(new FundsTransferredEvent(transfer));

            // TODO: likely handle database persistence in the Infrastructure layer.
            // Use the repository to update the accounts in the database
            await _accountRepository.UpdateBankAccountAsync(sourceAccount);
            await _accountRepository.UpdateBankAccountAsync(destinationAccount);


        }

        public decimal GetAccountBalance(BankAccount account)
        {
            // Business logic for retrieving the balance of an account

            // Validate inputs
            ValidateAccount(account);

            // Optionally,   perform additional logic before returning the balance.

            // Example: Checking for account status or performing additional business rules.

            return account.Balance;
        }

        public async Task<List<Transfer>> GetTransferHistory(BankAccount account)
        {
            // Business logic for retrieving the transfer history of an account

            // Validate inputs
            ValidateAccount(account);

            // Optionally,   perform additional logic before returning the transfer history.

            // Example: Filtering or sorting the transfer history based on business rules.
            account.Transfers = await _transferRepository.GetTransferHistoryAsync(account.Id);
            return account.Transfers;
        }

        public async Task<BankAccount> GetAccountByAccountNumber(string accountNumber)
        {
            // Application-specific logic for retrieving an account by account number

            // Validate input
            ValidateAccountNumber(accountNumber);

            // Use the repository to get the account by account number
            var account = await _accountRepository.GetBankAccountByAccountNumberAsync(accountNumber);


            return account;
        }

        private void ValidateAccountNumber(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Account number cannot be null or empty.", nameof(accountNumber));
            }
        }

        private string GenerateAccountNumber()
        {
            // Placeholder for generating a unique account number
            // This could involve some logic to ensure uniqueness

            // In a real-world scenario, you might use a combination of timestamp, customer information, or other factors
            // to create a unique and secure account number.

            // For demonstration purposes, a simple random number generator is used here.
            Random random = new Random();
            return random.Next(100000000, 999999999).ToString();
        }

        private void ValidateTransferInputs(BankAccount sourceAccount, BankAccount destinationAccount, decimal amount)
        {
            if (sourceAccount == null || destinationAccount == null)
            {
                throw new ArgumentNullException("Source and destination accounts cannot be null.");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be a positive amount.", nameof(amount));
            }

            if (sourceAccount == destinationAccount)
            {
                throw new InvalidOperationException("Source and destination accounts cannot be the same.");
            }
        }

        private void ValidateAccount(BankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }
        }
    }
}
