using SchedulerLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsService
{
    class ShedulerService
    {
        public void Start()
        {
            new SchedulerSendMail().SendEmails().GetAwaiter().GetResult();
        }
        public void Stop()
        {
        }
    }
}
