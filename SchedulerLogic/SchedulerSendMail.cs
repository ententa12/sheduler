using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using NLog;
using Quartz;
using Quartz.Impl;

namespace SchedulerLogic
{
    public class SchedulerSendMail
    {
        private readonly ILogger _logger;
        private const int ToSendInInterval = 100;

        public SchedulerSendMail()
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            _logger = serviceProvider.GetService<ILogger>();
        }

        public async Task SendEmails()
        {
            try
            {
                var factory = new StdSchedulerFactory();
                var scheduler = await factory.GetScheduler();
                var trigger = TriggerBuilder
                    .Create()
                    .WithIdentity(Guid.NewGuid().ToString())
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(1)
//                        .WithIntervalInMinutes(1)
                        .RepeatForever())
                    .Build();
                await scheduler.Start();
                await scheduler.ScheduleJob(CreateJobWithMail(ToSendInInterval), trigger);
            }
            catch (SchedulerException se)
            {
                _logger.Error(se);
            }
        }

        private IJobDetail CreateJobWithMail(int sendCount)
        {
            _logger.Info("In Job");
            return JobBuilder.Create<SendMailJob>()
                .WithIdentity(Guid.NewGuid().ToString())
                .SetJobData(new JobDataMap(new Dictionary<string, int>() {{"sendCount", sendCount}}))
                .Build();
        }
    }
}