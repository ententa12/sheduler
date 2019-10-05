using System.Linq;
using System.Threading.Tasks;
using CSVEmailModel;
using CSVReaderInterface;
using DIConfiguration;
using EmailSenderInterface;
using MailDatabaseInterface;
using Ninject;
using NLog;
using Quartz;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        ILogger _logger;
        IDatabaseContext<EmailPerson> _context;
        IEmailSender<EmailPerson> _emailSender;
        ICsvReader<EmailPerson> _csvReader;

        public SendMailJob()
        {
            var kernel = new StandardKernel(new Bindings());
            _logger = kernel.Get<ILogger>();
            _context = kernel.Get<IDatabaseContext<EmailPerson>>();
            _emailSender = kernel.Get<IEmailSender<EmailPerson>>();
            _csvReader = kernel.Get<ICsvReader<EmailPerson>>();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var countMailsToSend = (int) context.JobDetail.JobDataMap.Get("sendCount");
            var toSkip = _context.HigherIndex();
            _logger.Info("Last index: " + toSkip.ToString());
            var emails = _csvReader.ReadCsv("EmailList.csv", countMailsToSend, toSkip);
            var sendMails = emails
                .Where(e => !_context.CheckIfExist(e))
                .Select(e =>
                {
                    _context.Save(e);
                    _logger.Debug("Save item in database with id" + e.Id);
                    return _emailSender.SendEmail(e);
                });
            await Task.WhenAll(sendMails);
            _context.Dispose().Start();
        }
    }
}