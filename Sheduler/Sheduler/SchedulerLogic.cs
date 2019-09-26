using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sheduler.Sheduler
{
    class SchedulerLogic
    {
        public static async Task SendEmails()
        {
            Logger logger = LogManager.GetLogger("fileLogger");
            
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
    }
}
