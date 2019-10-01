using CSVEmailModel;
using CSVReaderLogic;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerLogic
{
    public class SchedulerSendMail
    {
        //static Logger logger = LogManager.GetLogger("fileLogger");
        public async Task SendEmails()
        {
            var emails = new CsvEmailReader<EmailPerson>().ReadCsv("C:\\csv\\EmailList.csv" , 0, 0);
            try
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();
                var mailTasks = emails.Select(mail => scheduler.ScheduleJob(CreateJobWithMail(mail), TriggerBuilder.Create()
                    .WithIdentity(Guid.NewGuid().ToString())
                    .StartNow()
                    .WithSimpleSchedule()
                    .Build()));
                await Task.WhenAll(mailTasks);
            }
            catch (SchedulerException se)
            {
                //logger.Error(se);
            }
        }

        static IJobDetail CreateJobWithMail(EmailPerson emailBody)
        {
            //logger.Info("In Job");
            return JobBuilder.Create<SendMailJob>()
                    .WithIdentity(Guid.NewGuid().ToString())
                    .SetJobData(new JobDataMap(new Dictionary<String, EmailPerson>() { { "Mail", emailBody } }))
                    .Build();
        }
    }
}
