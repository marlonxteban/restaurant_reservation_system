namespace rrs.DB.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }
        public required int RoleId { get; set; }
        public virtual required User User { get; set; }
        public virtual required Role Role { get; set; }
    }
}
