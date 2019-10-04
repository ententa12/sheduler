using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using CSVEmailModel;
using CSVReaderLogic;
using NLog;
using Quartz;
using Quartz.Impl;
using Logger = NLogger.Logger;

namespace SchedulerLogic
{
    public class SchedulerSendMail
    {
        ILogger _logger;
        int _toSendInInterval = 100;

        public SchedulerSendMail()
        {
            _logger = new Logger().GetLogger();
        }

        public async Task SendEmails()
        {
            try
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                ITrigger trigger = TriggerBuilder
                    .Create()
                    .WithIdentity(Guid.NewGuid().ToString())
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInMinutes(1)
                        .RepeatForever())
                    .Build();
                await scheduler.Start();
                await scheduler.ScheduleJob(CreateJobWithMail(_toSendInInterval), trigger);
            }
            catch (SchedulerException se)
            {
                _logger.Error(se);
            }
        }

        IJobDetail CreateJobWithMail(int sendCount)
        {
            _logger.Info("In Job");
            return JobBuilder.Create<SendMailJob>()
                .WithIdentity(Guid.NewGuid().ToString())
                .SetJobData(new JobDataMap(new Dictionary<String, int>() {{"sendCount", sendCount}}))
                .Build();
        }
    }
}