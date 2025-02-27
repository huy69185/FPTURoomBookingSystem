using System;
using System.Collections.Generic;

namespace ClassroomBooking.Repository.Entities;

public partial class Students
{
    public string StudentCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Department { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
