using PosLibrary.Model.Entities.Items;

namespace PosLibrary.Model.Entities.Transactions
{
    public class TransactionLines : CommonEntity
    {
        public string ReceiptId { get; set; }
        public int ItemId { get; set; }
        public int TransactionHeaderId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }

        public virtual Item Item { get; set; }
        public virtual TransactionHeader TransactionHeader { get; set; }
    }
}
