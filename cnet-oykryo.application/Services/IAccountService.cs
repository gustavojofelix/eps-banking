using cnet_oykryo.domain.Entities;

namespace cnet_oykryo.application.Services
{
    public interface IAccountService
    {
        Task CreateAccount(Customer customer, decimal initialDeposit);
        decimal GetAccountBalance(BankAccount account);
        Task<BankAccount> GetAccountByAccountNumber(string accountNumber);
        List<Transfer> GetTransferHistory(BankAccount account);
        Task TransferAmount(BankAccount sourceAccount, BankAccount destinationAccount, decimal amount);
    }
}