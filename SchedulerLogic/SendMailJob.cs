using System.Linq;
using System.Threading.Tasks;
using CSVEmailModel;
using DIConfiguration;
using EmailSenderInterface;
using MailDatabaseInterface;
using MediatR;
using MediatR.Ninject;
using MessagingLogic;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using NLog;
using Quartz;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        private readonly ILogger _logger;
        private readonly IDatabaseContext<EmailPerson> _context;
        private readonly IMediator _mediator;

        public SendMailJob()
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            _logger = serviceProvider.GetService<ILogger>();
            _context = serviceProvider.GetService<IDatabaseContext<EmailPerson>>();
            _mediator = serviceProvider.GetService<IMediator>();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var countMailsToSend = (int) context.JobDetail.JobDataMap.Get("sendCount");
            var toSkip = _context.LastIndex();
            _logger.Info("Last index: {0}", toSkip);
            var emails = await _mediator.Send(new ReadCsvRequest("EmailList.csv", countMailsToSend, toSkip));
            var sendMails = emails
                .Where(e => !_context.CheckIfExist(e))
                .Select(e =>
                {
                    _mediator.Publish(new SaveInDatabaseRequest(e));
                    _logger.Debug("Save item in database with id: {0}", e.Id);
                    return _mediator.Send(new SendMailRequest(e));
                });
            await Task.WhenAll(sendMails);
            _context.Dispose().Start();
        }
    }
}