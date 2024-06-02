using Application.Features.Mail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mail.Interfaces
{
    public interface IMailServiceProvider
    {
        Task Send(string messageString, string titleString, string address, CancellationToken token);
    }
}
