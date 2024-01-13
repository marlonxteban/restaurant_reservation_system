namespace rrs.DB.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int TableId { get; set; }
        public DateTime Date { get; set; }

        public string? Comment { get; set; }

        public virtual User? User { get; set; }
        public virtual Table? Table { get; set; }
    }
}
