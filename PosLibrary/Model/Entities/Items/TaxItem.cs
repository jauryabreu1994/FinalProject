using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosLibrary.Model.Entities.Items
{
    public class TaxItem : CommonEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0;

        public virtual ICollection<Item> Items { get; set; }

    }
}
