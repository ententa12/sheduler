using Akka.Actor;
using CSVEmailModel;
using CSVReaderLogic;
using NLog;
using SchedulerLogic;

namespace MessagingLogic
{
    public class ReadCsvActor : ReceiveActor
    {
        public ReadCsvActor(ILogger logger)
        {
            Receive<ReadCsvRequest>(message => {
                logger.Info("Path {0}, C {1}, TS {2}", message.Path, message.Count, message.ToSkip);
                var res = new CsvEmailReader<EmailPerson>().ReadFile(message.Path, message.Count, message.ToSkip);
                //_busClient.PublishAsync(new EmailsToSendRequest(res));
            });
        }
    }
}