using PosLibrary.Model.Entities.Vendors;

namespace PosLibrary.Model.Entities.Items
{
    public class Item : CommonEntity
    {
        public string Sku { get; set; } = string.Empty;
        public int ItemTaxId { get; set; } = 0;
        public int ItemDepartmentId { get; set; } = 0; 
        public int? VendorId { get; set; } = 0;
        public int? ItemDiscountid { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0;
        public virtual ItemTax ItemTax { get; set; }
        public virtual ItemDepartment ItemDepartment { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ItemDiscount ItemDiscount { get; set; }
    }
}
