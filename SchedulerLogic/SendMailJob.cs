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

        public SendMailJob()
        {
            _logger = new Logger().GetLogger();
            _context = new DatabaseLogic();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var toSkip = (int) context.JobDetail.JobDataMap.Get("toSkip");
            _logger.Info(toSkip.ToString() + DateTime.Now);
            var emails = new CsvEmailReader<EmailPerson>()
                .ReadCsv("C:\\csv\\EmailList.csv", 100, toSkip * 100);
            emails.Skip(1).ToList().ForEach(p =>
            {
                _context.Save(p);
                _logger.Info("Save item in database" + p.Email);
                _logger.Info(p.ToString() + DateTime.Now);
            });
            EmailSender emailSender = new EmailSender();
            var sendMails = emails.Where(e => !_context.CheckIfExist(e)).Select(e =>
            {
                _context.Save(e);
                _logger.Info("Save item in database" + e.Email);
                return emailSender.SendEmail(e);
            });
            await Task.WhenAll(sendMails);
//            await new Task(new Action(() => Console.WriteLine(toSkip)));
//            await new EmailSender().SendEmail(emailBody);
        }
    }
}