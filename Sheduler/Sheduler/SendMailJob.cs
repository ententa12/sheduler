using Quartz;
using Sheduler.Model;
using System.Threading.Tasks;

namespace Sheduler.Sheduler
{
    class SendMailJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var emailBody = (EmailPerson)context.JobDetail.JobDataMap.Get("Mail");
            LoggerUtils.logger.Info("Start sending mail - id: " + emailBody.Id);
            await EmailSender.SendEmail(emailBody);
            LoggerUtils.logger.Info("Stop sending mail - id: " + emailBody.Id);
        }
    }
}
