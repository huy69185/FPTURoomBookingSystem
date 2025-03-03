public class BookingCreateModel
{
    public string StudentCode { get; set; } = null!;
    public int ClassroomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Purpose { get; set; } = null!;
}
