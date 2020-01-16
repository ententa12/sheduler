using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using MessagingLogic;
using NLog;
using RawRabbit;
using SchedulerLogic;

namespace CSVReaderLogic
{
    public class ReadCsvHandler : IHandler<ReadCsvRequest>
    {
        private readonly IBusClient _busClient;
        private readonly ILogger _logger;

        public ReadCsvHandler(IBusClient busClient, ILogger logger)
        {
            _busClient = busClient;
            _logger = logger;
        }

        public async Task HandleAsync(ReadCsvRequest message, CancellationToken token)
        {
            _logger.Info("Path {0}, C {1}, TS {2}", message.Path, message.Count, message.ToSkip);
            var res = new CsvEmailReader<EmailPerson>().ReadFile(message.Path, message.Count, message.ToSkip);
            foreach (var emailPerson in res)
            {
                await _busClient.PublishAsync(new EmailsToSendRequest(emailPerson));
            }
        }
    }
}