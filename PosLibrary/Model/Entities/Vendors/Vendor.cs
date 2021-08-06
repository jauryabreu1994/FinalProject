using PosLibrary.Model.Entities.Items;
using System.Collections.Generic;

namespace PosLibrary.Model.Entities.Vendors
{
    public class Vendor
    {
        public string VendorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string VatNumber { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Item> Items { get; set; } = null;
    }
}
