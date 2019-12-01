using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using MediatR;
using MessagingLogic;
using RawRabbit;
using SchedulerLogic;

namespace CSVReaderLogic
{
    public class ReadCsvHandler : IHandler<ReadCsvRequest>
    {
        private readonly IBusClient _client;
        public ReadCsvHandler(IBusClient client)
        {
            _client = client;
        }

        public Task HandleAsync(ReadCsvRequest message, CancellationToken token)
        {
            var res = new CsvEmailReader<EmailPerson>().ReadFile(message.Path, message.Count, message.ToSkip);
            _client.PublishAsync(new EmailsToSend(res));
            return Task.CompletedTask;
        }
    }
}