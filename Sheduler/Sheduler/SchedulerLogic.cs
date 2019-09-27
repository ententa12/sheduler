using NLog;
using Quartz;
using Quartz.Impl;
using Sheduler.EmailReader;
using Sheduler.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sheduler.Sheduler
{
    class SchedulerLogic
    {
        static Logger logger = LogManager.GetLogger("fileLogger");
        public static async Task SendEmails()
        {
            var emails = CsvEmailReader<EmailPerson>.ReadCsv();
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();
                logger.Info("Start Scheduler action!");
                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<SendMailJob>()
                    .WithIdentity("job1", "group1")
                    .Build();
                logger.Info("Add Job");
                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(5)
                        .RepeatForever())
                    .Build();
                logger.Info("Add trigger");
                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                logger.Error(se);
            }
        }

        public static async Task SendEmails2()
        {
            var emails = CsvEmailReader<EmailPerson>.ReadCsv();
            try
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();
    //            emails.ForEach(email =>
    //            {
    //                ITrigger trigger = TriggerBuilder.Create()
    //.WithIdentity("trigger1")
    //.StartNow()
    //.WithSimpleSchedule(x => x
    //    .WithIntervalInSeconds(1))
    //.Build();
    //                await scheduler.ScheduleJob(CreateJobWithMail(email), trigger);
    //            });
                var mailTasks = emails.Select(mail => scheduler.ScheduleJob(CreateJobWithMail(mail), TriggerBuilder.Create()
                    .WithIdentity(Guid.NewGuid().ToString())
                    .StartNow()
                    .WithSimpleSchedule()
                    .Build()));
                await Task.WhenAll(mailTasks);
                //await scheduler.ScheduleJob(trigger);
            }
            catch (SchedulerException se)
            {
                logger.Error(se);
            }
        }

        static IJobDetail CreateJobWithMail(EmailPerson emailBody)
        {
            logger.Info("In Job");
            return JobBuilder.Create<SendMailJob>()
                    .WithIdentity(Guid.NewGuid().ToString())
                    .SetJobData(new JobDataMap(new Dictionary<String, EmailPerson>() { { "Mail", emailBody } }))
                    .Build();
        }
    }
}
