using CSVEmailModel;
using MailDatabaseInterface;
using NLog;
using Quartz;
using Quartz.Spi;
using RawRabbit;

namespace SchedulerLogic
{
    public class SendJobFactory : IJobFactory
    {
        private readonly ILogger _logger;
        private readonly IDatabaseContext<EmailPerson> _context;
        private readonly IBusClient _client;

        public SendJobFactory(ILogger logger, IDatabaseContext<EmailPerson> context, IBusClient client)
        {
            _logger = logger;
            _context = context;
            _client = client;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return new SendMailJob(_logger, _context, _client);
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}