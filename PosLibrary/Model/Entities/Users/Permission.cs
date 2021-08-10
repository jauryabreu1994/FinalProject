using System.Collections.Generic;

namespace PosLibrary.Model.Entities.User
{
    public class Permission : CommonEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
