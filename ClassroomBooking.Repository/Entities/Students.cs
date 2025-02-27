using System;
using System.Collections.Generic;

namespace ClassroomBooking.Repository.Entities;

public partial class Students
{
    public string StudentCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Campus { get; set; }
    public string Password { get; set; }
    public int Role {  get; set; }
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
