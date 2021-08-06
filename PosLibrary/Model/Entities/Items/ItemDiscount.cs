using System.Collections.Generic;

namespace PosLibrary.Model.Entities.Items
{
    public class ItemDiscount : CommonEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal AmountPercent { get; set; } = 0;

        public virtual ICollection<Item> Items { get; set; } = null;

    }
}
