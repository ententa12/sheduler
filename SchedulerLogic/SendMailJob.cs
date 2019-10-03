using System;
using System.Linq;
using System.Threading.Tasks;
using CSVEmailModel;
using CSVReaderLogic;
using EmailSenderLogic;
using MailDatabase;
using MailDatabaseInterface;
using NLog;
using Quartz;
using Logger = NLogger.Logger;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        ILogger _logger;
        IDatabaseContext<EmailPerson> _context;
        EmailSender _emailSender;

        public SendMailJob()
        {
            _logger = new Logger().GetLogger();
            _context = new DatabaseLogic();
            _emailSender = new EmailSender();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var toSkip = _context.HigherIndex();
            _logger.Info("Last index: " + toSkip.ToString());
            var emails = new CsvEmailReader<EmailPerson>()
                .ReadCsv("C:\\csv\\EmailList.csv", 100, toSkip);
            var sendMails = emails
                .Where(e => !_context.CheckIfExist(e))
                .Select(e =>
                {
                    _context.Save(e);
                    _logger.Info("Save item in database " + e.Email);
                    return _emailSender.SendEmail(e);
                });
            await Task.WhenAll(sendMails);
        }
    }
}