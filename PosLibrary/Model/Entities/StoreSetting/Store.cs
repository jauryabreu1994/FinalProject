namespace PosLibrary.Model.Entities.StoreSetting
{
    public class Store : CommonEntity
    {
        public string Name { get; set; } = "";
        public string VatNumber { get; set; } = "";
        public string CompanyName { get; set; }
        public string Address { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public int ReceiptId { get; set; } = 1;
        public int CustomerId { get; set; } = 1;
        public int VendorId { get; set; } = 1;
    }
}
