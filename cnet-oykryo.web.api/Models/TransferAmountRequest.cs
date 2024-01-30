namespace cnet_oykryo.web.api.Models
{
    public class TransferAmountRequest
    {
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
