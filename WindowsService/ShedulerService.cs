using SchedulerLogic;

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