using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Sneaker_Bar.Hubs
{
    public class NotificationHub : Hub
    {
        public void Func() { 
        
        }

    }
    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity.Name;
        }
    }

}
