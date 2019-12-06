using System.Threading;
using CSVReaderLogic;
using DIConfiguration;
using MessagingLogic;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using SchedulerLogic;

namespace WindowsService
{
    class ShedulerService
    {
        public void Start()
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            var client = serviceProvider.GetService<IBusClient>();
            client.SubscribeAsync<ReadCsvRequest>(async (msg, t) =>
            {
                await new ReadCsvHandler().HandleAsync(msg, CancellationToken.None);
            });
            client.SubscribeAsync<EmailsToSendRequest>(async (msg, t) =>
            {
                await new SaveInDatabaseHandler().HandleAsync(msg, CancellationToken.None);
            });
            client.SubscribeAsync<EmailsToSendRequest>(async (msg, t) =>
            {
                await new SendMailHandler().HandleAsync(msg, CancellationToken.None);
            });
            new SchedulerSendMail().SendEmails().GetAwaiter().GetResult();
        }

        public void Stop()
        {
        }
    }
}