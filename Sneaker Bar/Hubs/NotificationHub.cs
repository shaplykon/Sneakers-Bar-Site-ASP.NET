using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task Send(string message)
        {
            var user = Context.User;
            var userName = user.Identity.Name;
            
            await Clients.All.SendAsync("Send", message, userName);
        }
    }
}
