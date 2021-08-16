namespace PosLibrary.Model.Entities.Users
{
    public class GroupPermission : CommonEntity
    {
        public int UserGroupId { get; set; } = 1;
        public string PermissionCode { get; set; } = string.Empty;
        public virtual UserGroup UserGroup { get; set; }

    }
}
