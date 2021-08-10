using PosLibrary.Model.Entities.Payments;

namespace PosLibrary.Model.Entities.Transactions
{
    public class TransactionPayments : CommonEntity
    {
        public string ReceiptId { get; set; }
        public int PaymentMethodId { get; set; }
        public int TransactionHeaderId { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual TransactionHeader TransactionHeader { get; set; }
    }
}
