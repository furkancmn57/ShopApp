using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mail.Models
{
    public class Settings
    {
        public string TemplateHtml { get; set; }
        public string Title { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
        public int? Port { get; set; }
        public string FromAddress { get; set; }
        public List<string> ToAddresses { get; set; }
        public Enums.MailServiceProvider Provider { get; set; } = Enums.MailServiceProvider.Smtp;
    }
}
