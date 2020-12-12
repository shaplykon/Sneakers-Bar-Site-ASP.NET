using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sneaker_Bar.Services.UserConnections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sneaker_Bar.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {      
        IUserConnectionManager userConnectionManager;
        public NotificationHub(IUserConnectionManager _userConnectionManager) {
            userConnectionManager = _userConnectionManager;
        }

        public override Task OnConnectedAsync()
        {
            userConnectionManager.ConnectUser(Context.User.Identity.Name, Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
           userConnectionManager.DisconnectUser(Context.User.Identity.Name, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
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
