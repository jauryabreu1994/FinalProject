namespace PosLibrary.Model.Entities.User
{
    public class GroupPermission : CommonEntity
    {
        public int UserGroupId { get; set; }
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual UserGroup UserGroup { get; set; }

    }
}
