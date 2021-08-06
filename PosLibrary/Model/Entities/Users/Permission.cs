using System.Collections.Generic;

namespace PosLibrary.Model.Entities.User
{
    public class Permission : CommonEntity
    {
        public string Name { get; set; }

        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
