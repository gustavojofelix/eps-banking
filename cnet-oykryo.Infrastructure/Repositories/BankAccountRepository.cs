using cnet_oykryo.domain.Entities;
using cnet_oykryo.domain.Services;
using cnet_oykryo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.Infrastructure.Repositories
{
    public class BankAccountRepository: IBankAccountRepository
    {
        private readonly EPSDBContext _dbContext;

        public BankAccountRepository(EPSDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BankAccount> GetBankAccountByIdAsync(int accountId)
        {
            return await _dbContext.BankAccounts.FindAsync(accountId);
        }

        public async Task<List<BankAccount>> GetAccountsByCustomerIdAsync(int customerId)
        {
            return await _dbContext.BankAccounts
                .Where(account => account.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<BankAccount> GetBankAccountByAccountNumberAsync(string accountNumber)
        {
            if (accountNumber is null)
            {
                throw new ArgumentNullException(nameof(accountNumber));
            }

            return await _dbContext.BankAccounts
                .FirstOrDefaultAsync(account => account.AccountNumber == accountNumber);
        }

        public async Task AddBankAccountAsync(BankAccount bankAccount)
        {
            _dbContext.BankAccounts.Add(bankAccount);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBankAccountAsync(BankAccount bankAccount)
        {
            _dbContext.BankAccounts.Update(bankAccount);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBankAccountAsync(int accountId)
        {
            var bankAccount = await _dbContext.BankAccounts.FindAsync(accountId);
            if (bankAccount != null)
            {
                _dbContext.BankAccounts.Remove(bankAccount);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
