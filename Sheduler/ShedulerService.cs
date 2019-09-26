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
            logger.Info("Start Scheduler");
            SchedulerLogic.SendEmails().GetAwaiter().GetResult();
        }
        public void Stop()
        {
            // write code here that runs when the Windows Service stops.  
        }
    }
}
