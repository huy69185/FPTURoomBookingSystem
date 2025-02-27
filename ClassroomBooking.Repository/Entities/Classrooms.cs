using System;
using System.Collections.Generic;

namespace ClassroomBooking.Repository.Entities;

public partial class Classrooms
{
    public int ClassroomId { get; set; }

    public string ClassroomName { get; set; } = null!;

    public string BuildingName { get; set; } = null!;

    public int Capacity { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
