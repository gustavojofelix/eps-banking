using cnet_oykryo.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.domain.Services
{
    public interface ITransferRepository
    {
        Task<Transfer> GetTransferByIdAsync(int transferId);
        Task<List<Transfer>> GetTransfersByAccountIdAsync(int accountId);
        Task AddTransferAsync(Transfer transfer);
        Task<List<Transfer>> GetTransferHistoryAsync(int accountId);
    }
}
