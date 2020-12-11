
using Microsoft.Extensions.Options;
using Sneaker_Bar.Configuration;
using Sneaker_Bar.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using MailKit.Net.Smtp;
using System.Net.Mail;
using MimeKit;

namespace Sneaker_Bar.Services
{
    public class MailService : IMailServicer
    {
        private readonly SmtpSettings _settings;
        public MailService(IOptions<SmtpSettings> options) {

            _settings = options.Value;
        }
        public async Task SendMessage(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", _settings.From));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(_settings.Server, _settings.Port, false);
                await client.AuthenticateAsync(_settings.From, _settings.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }          
        }

        public string BuildConfirmationMessage(string email, string date)
        {
            string message = "<h1> <p>" + email + ", our order was successfully confrirmed!</p>\n";
            message += "<p>Order date: " + date;
            return message;
        }
        public string BuildPreConfirmationMessage(string email, string date, List<Sneakers> orderList, string hostUrl, string id)
        {
            string message = "<h1> <p>" + email + ", check your order information!</p>\n";

            message += "<ol>";
            double totalPrice = 0;
            foreach (Sneakers sneakers in orderList)
            {
                message += "<li>";
                message += sneakers.Company + " ";
                message += sneakers.Model;
                message += "</li>";
                totalPrice += sneakers.Price;
            }
            message += "</ol>";
            message += "<p>Total price is " + totalPrice.ToString() + "$ (" + orderList.Count + " items)" + "</p>";
            message += "<p>Order date: " + date;
            message += "<p><a href = \""  + hostUrl + "/Sneakers/OrderConfirmation/" +id +
                "\">Click this link to confirm your order!</ a >";
            return message;
        }

    }
}
