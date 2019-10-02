using System;
using System.Linq;
using System.Threading.Tasks;
using CSVEmailModel;
using CSVReaderLogic;
using EmailSenderLogic;
using Quartz;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var emailBody = (int) context.JobDetail.JobDataMap.Get("toSkip");
            var emails = new CsvEmailReader<EmailPerson>()
                .ReadCsv("C:\\csv\\EmailList.csv", 100, emailBody * 100);
            emails.ForEach(p => Console.WriteLine(p.ToString()));
            await new EmailSender().SendEmail(emails[0]);
        }
    }
}