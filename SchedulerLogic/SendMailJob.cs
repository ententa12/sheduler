using System.Threading.Tasks;
using Akka.Actor;
using CSVEmailModel;
using MailDatabaseInterface;
using MessagingLogic;
using NLog;
using Quartz;
using RawRabbit;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        private readonly ILogger _logger;
        private readonly IDatabaseContext<EmailPerson> _context;

        public SendMailJob(ILogger logger, IDatabaseContext<EmailPerson> context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var countMailsToSend = (int) context.JobDetail.JobDataMap.Get("sendCount");
            var toSkip = _context.LastIndex();
            _logger.Info("Last index: {0}", toSkip);
            var system = ActorSystem.Create("ReadCsvSystem");
            var props = Props.Create(() => new ReadCsvActor(_logger));
            var actor = system.ActorOf(props, "ReadCsvActor");
            await Task.Run(() => actor.Tell(new ReadCsvRequest("EmailList.csv", toSkip, countMailsToSend)));
        }
    }
}