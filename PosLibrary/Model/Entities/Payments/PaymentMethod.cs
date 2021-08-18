using PosLibrary.Model.Entities.Transactions;
using System.Collections.Generic;

namespace PosLibrary.Model.Entities.Payments
{
    public class PaymentMethod : CommonEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool ToSales { get; set; } = false;
        public bool ToReturn { get; set; } = false;
        public bool OverTender { get; set; } = false;
        public bool UnderTender { get; set; } = false;
        public bool IsMainTender { get; set; } = false;
        public virtual ICollection<TransactionPayments> TransactionPayments { get; set; }
    }
}
