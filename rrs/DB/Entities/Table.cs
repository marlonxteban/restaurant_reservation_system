namespace rrs.DB.Entities
{
    public class Table
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }
        public string? Location { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
