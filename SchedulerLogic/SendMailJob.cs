﻿using System.Linq;
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
        private readonly ILogger _logger;
        private readonly IDatabaseContext<EmailPerson> _context;
        private readonly IEmailSender<EmailPerson> _emailSender;
        private readonly IDataReader<EmailPerson> _csvReader;

        public SendMailJob()
        {
            var kernel = new StandardKernel(new Bindings());
            _logger = kernel.Get<ILogger>();
            _context = kernel.Get<IDatabaseContext<EmailPerson>>();
            _emailSender = kernel.Get<IEmailSender<EmailPerson>>();
            _csvReader = kernel.Get<IDataReader<EmailPerson>>();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var countMailsToSend = (int) context.JobDetail.JobDataMap.Get("sendCount");
            var toSkip = _context.LastIndex();
            _logger.Info("Last index: " + toSkip);
            var emails = _csvReader.ReadFile("EmailList.csv", countMailsToSend, toSkip);
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