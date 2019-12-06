using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using DIConfiguration;
using MediatR;
using MessagingLogic;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using RabbitMQ.Client.Impl;
using RawRabbit;
using SchedulerLogic;

namespace CSVReaderLogic
{
    public class ReadCsvHandler : IHandler<ReadCsvRequest>
    {
        public async Task HandleAsync(ReadCsvRequest message, CancellationToken token)
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            var client = serviceProvider.GetService<IBusClient>();
            serviceProvider.GetService<ILogger>().Info("Path {0}, C {1}, TS {2}", message.Path, message.Count, message.ToSkip);
            var res = new CsvEmailReader<EmailPerson>().ReadFile(message.Path, message.Count, message.ToSkip);
            await client.PublishAsync(new EmailsToSendRequest(res));
        }
    }
}