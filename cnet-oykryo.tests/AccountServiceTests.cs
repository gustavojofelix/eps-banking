using cnet_oykryo.application.Services;
using cnet_oykryo.domain.Entities;
using cnet_oykryo.domain.Services;
using cnet_oykryo.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.tests
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task CreateAccount_Should_Create_Account()
        {
            // Arrange
            var mockRepository = new Mock<IBankAccountRepository>();
            var accountService = new AccountService(mockRepository.Object);

            var customer = new Customer { Id = 1, Name = "John Doe" };
            var initialDeposit = 1000m;

            // Act
            await accountService.CreateAccount(customer, initialDeposit);

            // Assert
            // Verify that the repository's AddBankAccountAsync method was called with the correct parameters.
            mockRepository.Verify(repo => repo.AddBankAccountAsync(It.IsAny<BankAccount>()), Times.Once);
        }

        [Fact]
        public async Task TransferAmount_Should_Transfer_Funds()
        {
            // Arrange
            var mockRepository = new Mock<IBankAccountRepository>();
            var accountService = new AccountService(mockRepository.Object);

            var sourceAccount = new BankAccount { Id = 1, Balance = 1000m };
            var destinationAccount = new BankAccount { Id = 2, Balance = 500m };
            var amount = 200m;

            // Act
            await accountService.TransferAmount(sourceAccount, destinationAccount, amount);

            // Assert
            // Verify that the repository's UpdateBankAccountAsync method was called for both accounts.
            mockRepository.Verify(repo => repo.UpdateBankAccountAsync(sourceAccount), Times.Once);
            mockRepository.Verify(repo => repo.UpdateBankAccountAsync(destinationAccount), Times.Once);
        }

        [Fact]
        public void GetAccountBalance_Should_Return_Correct_Balance()
        {
            // Arrange
            var mockRepository = new Mock<IBankAccountRepository>();
            var accountService = new AccountService(mockRepository.Object);

            var account = new BankAccount { Id = 1, Balance = 1000m };

            // Act
            var balance = accountService.GetAccountBalance(account);

            // Assert
            Assert.Equal(1000m, balance);
        }

        [Fact]
        public async Task GetAccountByAccountNumber_Should_Return_Account()
        {
            // Arrange
            var mockRepository = new Mock<IBankAccountRepository>();
            var accountService = new AccountService(mockRepository.Object);

            var accountNumber = "123456789";
            var expectedAccount = new BankAccount { Id = 1, AccountNumber = accountNumber };

            // Setup mock repository to return the expected account.
            mockRepository.Setup(repo => repo.GetBankAccountByAccountNumberAsync(accountNumber))
                          .ReturnsAsync(expectedAccount);

            // Act
            var result = await accountService.GetAccountByAccountNumber(accountNumber);

            // Assert
            Assert.Equal(expectedAccount, result);
        }
    }
}
