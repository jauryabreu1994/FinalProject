using PosLibrary.Model.Entities.Transactions;
using System;
using System.Collections.Generic;

namespace PosLibrary.Model.Entities.Customers
{
    public class Customer : CommonEntity
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string VatNumber { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateBorn { get; set; }

        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; } = null;
    }
}
