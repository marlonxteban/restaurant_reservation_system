namespace rrs.DB.Entities
{
    public class Role
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
