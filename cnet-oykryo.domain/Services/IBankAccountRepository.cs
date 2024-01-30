using cnet_oykryo.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.domain.Services
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> GetBankAccountByIdAsync(int accountId);
        Task<List<BankAccount>> GetAccountsByCustomerIdAsync(int customerId);
        Task AddBankAccountAsync(BankAccount bankAccount);
        Task UpdateBankAccountAsync(BankAccount bankAccount);
        Task DeleteBankAccountAsync(int accountId);
        Task<BankAccount> GetBankAccountByAccountNumberAsync(string accountNumber);
    }
}
