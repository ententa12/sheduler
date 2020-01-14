using System.Threading.Tasks;
using CSVEmailModel;
using MailDatabaseInterface;
using NLog;
using Quartz;
using RawRabbit;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        private readonly ILogger _logger;
        private readonly IDatabaseContext<EmailPerson> _context;
        private readonly IBusClient _client;

        public SendMailJob(ILogger logger, IDatabaseContext<EmailPerson> context, IBusClient client)
        {
            _logger = logger;
            _context = context;
            _client = client;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var countMailsToSend = (int) context.JobDetail.JobDataMap.Get("sendCount");
            var toSkip = _context.LastIndex();
            _logger.Info("Last index: {0}", toSkip);
            await _client.PublishAsync(new ReadCsvRequest("EmailList.csv", toSkip, countMailsToSend));
        }
    }
}