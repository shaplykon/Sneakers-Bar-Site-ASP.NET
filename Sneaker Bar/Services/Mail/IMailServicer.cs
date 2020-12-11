using Sneaker_Bar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Services
{
    public interface IMailServicer
    {
        Task SendMessage(string email, string subject, string message);
        string BuildConfirmationMessage(string email, string date);
        string BuildPreConfirmationMessage(string email,  string date, List<Sneakers> orderList, string hostUrl, string id);
    }
}
