using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.domain.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }

        public BankAccount SourceAccount { get; set; }

        public BankAccount DestinationAccount { get; set; }
    }
}
