using Application.Common.Interfaces;
using Application.Features.Mail.Enums;
using Application.Features.Mail.Interfaces;
using Application.Features.Mail.Models;
using Infrastructure.Persistence.Repositories.MailProviders.Providers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.MailProviders
{
    public class MailProviderFactory : IMailProviderFactory
    {
        private readonly IConfiguration _configuration;

        public MailProviderFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMailServiceProvider GetProvider(Settings request)
        {
            IMailServiceProvider provider = null;

            switch (request.Provider)
            {
                case MailServiceProvider.Smtp:
                    provider =  new Smtp(_configuration);
                    break;
                default:
                    break;
            }


            return provider;
        }
    }
}
