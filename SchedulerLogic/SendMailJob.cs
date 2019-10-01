using CSVEmailModel;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerLogic
{
    class SendMailJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var emailBody = (EmailPerson)context.JobDetail.JobDataMap.Get("Mail");
            await EmailSender.SendEmail(emailBody);
        }
    }
}
