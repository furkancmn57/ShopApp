using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Application.Services.MailService
{
    public class MailManager : IMailService
    {
        private readonly IConfiguration _configration;

        public MailManager(IConfiguration configration)
        {
            _configration = configration;
        }

        public void SendMail(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ShopApp",_configration["Mail:Smtp:Username"]));
            message.To.Add(new MailboxAddress("",to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_configration["Smtp:Host"], Convert.ToInt32(_configration["Smtp:Port"]), true);
                client.Authenticate(_configration["Smtp:StmpUser"], _configration["Smtp:Password"]);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
