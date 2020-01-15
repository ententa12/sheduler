using CSVEmailModel;
using MailDatabaseInterface;
using MessagingLogic;
using NLog;
using Quartz;
using Quartz.Spi;

namespace SchedulerLogic
{
    public class SendJobFactory : IJobFactory
    {
        private readonly ILogger _logger;
        private readonly IDatabaseContext<EmailPerson> _context;

        public SendJobFactory(ILogger logger, IDatabaseContext<EmailPerson> context)
        {
            _logger = logger;
            _context = context;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return new SendMailJob(_logger, _context);
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}