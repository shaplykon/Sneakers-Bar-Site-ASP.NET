using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Services
{
    public interface IMessageSender
    {
        Task SendMessage(string email, string subject, string message);
    }
}
