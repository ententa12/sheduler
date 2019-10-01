using CSVEmailModel;
using Quartz;
using EmailSenderLogic;
using System.Threading.Tasks;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var emailBody = (EmailPerson)context.JobDetail.JobDataMap.Get("Mail");
            await new EmailSender().SendEmail(emailBody);
        }
    }
}
