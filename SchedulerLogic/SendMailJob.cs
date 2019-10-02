using System;
using System.Threading.Tasks;
using CSVEmailModel;
using CSVReaderLogic;
using NLog;
using Quartz;
using Logger = NLogger.Logger;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        ILogger _logger;

        public SendMailJob()
        {
            _logger = new Logger().GetLogger();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var toSkip = (int) context.JobDetail.JobDataMap.Get("toSkip");
            _logger.Info(toSkip.ToString() + DateTime.Now);
            var emails = new CsvEmailReader<EmailPerson>()
                .ReadCsv("C:\\csv\\EmailList.csv", 100, toSkip * 100);
            emails.ForEach(p => _logger.Info(p.ToString() + DateTime.Now));
            await new Task(new Action(() => Console.WriteLine(toSkip)));
//            await new EmailSender().SendEmail(emailBody);
        }
    }
}