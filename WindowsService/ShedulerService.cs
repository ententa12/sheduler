using SchedulerLogic;

namespace WindowsService
{
    public class ShedulerService
    {
        private SchedulerSendMail _sendMail;
        public ShedulerService(SchedulerSendMail sendMail)
        {
            _sendMail = sendMail;
        }

        public void Start()
        {
            _sendMail.SendEmails().GetAwaiter().GetResult();
        }

        public void Stop()
        {
        }
    }
}