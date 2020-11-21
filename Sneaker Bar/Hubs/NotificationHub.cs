using Microsoft.AspNetCore.SignalR;
using Sneaker_Bar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task Send(string company, string model)
        {
            string message = $"Welcome! New sneakers {company} {model} were added to our catalog";
            await Clients.Others.SendAsync("Send", message);
        }
    }
}
