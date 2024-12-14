using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Model;
using System;
using System.Threading.Tasks;

namespace PaymentConsumer.Consumers
{
    public class OrderPaymentConsumer : IConsumer<Pagamentos>
    {
        private readonly ILogger<OrderPaymentConsumer> logger;
        public OrderPaymentConsumer(ILogger<OrderPaymentConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<Pagamentos> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);

            logger.LogInformation($"Nova mensagem recebida :" +
                $" {context.Message.Price} {context.Message.Id}");
        }
    }
}
