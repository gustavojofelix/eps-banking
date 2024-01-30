using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
    }
}
