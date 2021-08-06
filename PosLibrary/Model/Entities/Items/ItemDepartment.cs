using System.Collections.Generic;

namespace PosLibrary.Model.Entities.Items
{
    public class ItemDepartment : CommonEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Item> Items { get; set; } = null;

    }
}
