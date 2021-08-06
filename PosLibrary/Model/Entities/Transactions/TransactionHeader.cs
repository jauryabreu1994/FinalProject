using PosLibrary.Model.Entities.Customers;
using System.Collections.Generic;

namespace PosLibrary.Model.Entities.Transactions
{
    public class TransactionHeader : CommonEntity
    {
        public string ReceiptId { get; set; }
        public int CustomerId { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalPayment { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<TransactionLines> TransactionLines { get; set; }
        public virtual ICollection<TransactionPayments> TransactionPayments { get; set; }

    }
}
