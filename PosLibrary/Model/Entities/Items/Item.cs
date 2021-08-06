using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosLibrary.Model.Entities.Items
{
    public class Item : CommonEntity
    {
        public string Sku { get; set; } = string.Empty;
        public int TaxItemId { get; set; } = 0;
        public int DepartmentItemId { get; set; } = 0; 
        public int VendorId { get; set; } = 0;
        public int? Discountid { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0;
        public virtual TaxItem TaxItem { get; set; }

    }
}
