using PosLibrary.Model.Entities.Transactions;
using System.Collections.Generic;

namespace PosLibrary.Model.Entities.Payments
{
    public class PaymentMethod : CommonEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<TransactionPayments> TransactionPayments { get; set; }
    }
}
