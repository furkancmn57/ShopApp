using Application.Common.Interfaces;
using Application.Features.Mail.Interfaces;
using Application.Features.Mail.Models;
using Domain.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RabbitMq.Consumers
{
    public class UpdateOrderConsumer : IConsumer<UpdateOrderMessage>
    {
        private readonly IMailProviderFactory _mailServiceProvider;

        public UpdateOrderConsumer(IMailProviderFactory mailServiceProvider)
        {
            _mailServiceProvider = mailServiceProvider;
        }

        public async Task Consume(ConsumeContext<UpdateOrderMessage> context)
        {
            var mailProvider = _mailServiceProvider.GetProvider(new Settings());
            await mailProvider.Send($"Siparişinizin durumu güncellendi. Yeni durum: {context.Message.Status}", "Sipariş Durumu Güncellendi", context.Message.Email,context.CancellationToken);
        }
    }
}
