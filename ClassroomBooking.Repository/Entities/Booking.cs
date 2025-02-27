using System;
using System.Collections.Generic;

namespace ClassroomBooking.Repository.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public string StudentCode { get; set; } = null!;

    public int ClassroomId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Purpose { get; set; } = null!;

    public string BookingStatus { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual Classrooms Classroom { get; set; } = null!;

    public virtual Students StudentCodeNavigation { get; set; } = null!;
}
