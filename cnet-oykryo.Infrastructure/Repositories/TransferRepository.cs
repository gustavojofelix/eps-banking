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
    public class TransferRepository: ITransferRepository
    {
        private readonly EPSDBContext _dbContext;

        public TransferRepository(EPSDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Transfer> GetTransferByIdAsync(int transferId)
        {
            return await _dbContext.Transfers.FindAsync(transferId);
        }

        public async Task<List<Transfer>> GetTransfersByAccountIdAsync(int accountId)
        {
            return await _dbContext.Transfers
                .Where(transfer => transfer.SourceAccountId == accountId || transfer.DestinationAccountId == accountId)
                .ToListAsync();
        }

        public async Task AddTransferAsync(Transfer transfer)
        {
            _dbContext.Transfers.Add(transfer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Transfer>> GetTransferHistoryAsync(int accountId)
        {
            return await _dbContext.Transfers
                .Where(transfer => transfer.SourceAccountId == accountId || transfer.DestinationAccountId == accountId)
                .OrderByDescending(transfer => transfer.TransferDate)
                .ToListAsync();
        }
    }
}
