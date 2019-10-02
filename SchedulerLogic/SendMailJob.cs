using System.Threading.Tasks;
using CSVEmailModel;
using EmailSenderLogic;
using Quartz;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var emailBody = (EmailPerson) context.JobDetail.JobDataMap.Get("Mail");
            await new EmailSender().SendEmail(emailBody);
        }
    }
}