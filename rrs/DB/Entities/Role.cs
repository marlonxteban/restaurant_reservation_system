namespace rrs.DB.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}
