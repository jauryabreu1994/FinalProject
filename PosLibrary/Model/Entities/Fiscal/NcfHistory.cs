namespace PosLibrary.Model.Entities.Fiscal
{
    public class NcfHistory : CommonEntity
    {
        public string ReceiptId { get; set; }
        public int NcfTypeId { get; set; }
        public string NcfNumber { get; set; }
        public string ReturnReceiptId { get; set; }
        public string ReturnNcfNumber { get; set; }
        public string VatNumber { get; set; }
        public string Company { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithTax { get; set; }
        public decimal TotalTax { get; set; }
        public bool TaxExempt { get; set; }
        public virtual NcfType NcfType { get; set; }
    }
}
