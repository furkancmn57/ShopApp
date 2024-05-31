using Application.Features.Mail.Interfaces;
using Application.Features.Mail.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.MailProviders.Providers
{
    public class Smtp : IMailServiceProvider
    {
        private readonly IConfiguration _configuration;

        public Smtp(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Send(string messageString, string titleString, string address)
        {
            MailMessage message = new MailMessage();

            message.Subject = titleString;
            message.Body = messageString;
            message.IsBodyHtml = false;
            message.To.Add(address);
            message.From = new MailAddress(_configuration["Smtp:Username"]);


            SmtpClient client = new SmtpClient();
            client.Host = _configuration["Smtp:Host"];
            client.Port = Convert.ToInt32(_configuration["Smtp:Port"]);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            await client.SendMailAsync(message);
        }
    }
}
