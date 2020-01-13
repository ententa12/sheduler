using CSVEmailModel;
using MailDatabaseInterface;
using MediatR;
using NLog;
using Quartz;
using Quartz.Spi;

namespace SchedulerLogic
{
    public class SendJobFactory : IJobFactory
    {
        private readonly ILogger _logger;
        private readonly IDatabaseContext<EmailPerson> _context;
        private readonly IMediator _mediator;

        public SendJobFactory(ILogger logger, IDatabaseContext<EmailPerson> context, IMediator mediator)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return new SendMailJob(_logger, _context, _mediator);
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}