using NLog;
using Sheduler.EmailReader;
using Sheduler.Model;
using Sheduler.Sheduler;
using System;
using System.Linq;

namespace Sheduler
{
    class ShedulerService
    {
        public void Start()
        {
            Logger logger = LogManager.GetLogger("fileLogger");
            logger.Info("Start was Scheduler");
            SchedulerLogic.SendEmails2().GetAwaiter().GetResult();
        }
        public void Stop()
        {
            LoggerUtils.logger.Info("Scheduler was stopped");
        }
    }
}
