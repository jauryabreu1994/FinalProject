using System.Collections.Generic;

namespace PosLibrary.Model.Entities.User
{
    public class UserGroup : CommonEntity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
