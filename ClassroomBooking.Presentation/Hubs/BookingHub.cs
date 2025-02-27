using Microsoft.AspNetCore.SignalR;

namespace ClassroomBooking.Presentation.Hubs
{
    public class BookingHub : Hub
    {
        // Ví dụ một method server để broadcast
        public async Task SendBookingNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveBookingNotification", message);
        }
    }
}
