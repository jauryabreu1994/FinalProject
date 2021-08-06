namespace PosLibrary.Model.Entities.User
{
    public class GroupPermission : CommonEntity
    {
        public int UserGroupId { get; set; } = 1;
        public int PermissionId { get; set; } = 1;

        public virtual Permission Permission { get; set; }
        public virtual UserGroup UserGroup { get; set; }

    }
}
