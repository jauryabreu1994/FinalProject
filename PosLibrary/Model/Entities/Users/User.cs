namespace PosLibrary.Model.Entities.User
{
    public class User : CommonEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int UserGroupId { get; set; } = 1;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string VatNumber { get; set; } = string.Empty;
        public byte Gender { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public virtual UserGroup UserGroup { get; set; }
    }
}
