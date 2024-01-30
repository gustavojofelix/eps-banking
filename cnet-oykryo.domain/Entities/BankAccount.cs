using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.domain.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }

        // Navigation property
        public Customer Customer { get; set; }

        public List<Transfer> Transfers { get; set; } = new List<Transfer>();
        public string AccountNumber { get;  set; }
    }
}
