using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Hub_
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveNotification", message);
        }
    }
}