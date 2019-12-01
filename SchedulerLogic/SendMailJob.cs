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
using RawRabbit;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        private readonly ILogger _logger;
        private readonly IDatabaseContext<EmailPerson> _context;
        private readonly IBusClient _client;

        public SendMailJob()
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            _logger = serviceProvider.GetService<ILogger>();
            _context = serviceProvider.GetService<IDatabaseContext<EmailPerson>>();
            _client = serviceProvider.GetService<IBusClient>();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var countMailsToSend = (int) context.JobDetail.JobDataMap.Get("sendCount");
            var toSkip = _context.LastIndex();
            _logger.Info("Last index: {0}", toSkip);
            await _client.PublishAsync(new ReadCsvRequest("EmailList.csv", countMailsToSend, toSkip));
        }
    }
}