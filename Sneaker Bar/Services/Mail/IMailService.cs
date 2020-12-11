using Sneaker_Bar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sneaker_Bar.Services
{
    public interface IMailService
    {
        Task SendMessage(string email, string subject, string message);
        string BuildConfirmationMessage(string email, string date);
        string BuildPreConfirmationMessage(string email,  string date, List<Sneakers> orderList, string hostUrl, string id);
    }
}
