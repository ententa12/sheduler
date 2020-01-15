using System.Threading;
using CSVReaderLogic;
using MessagingLogic;
using RawRabbit;
using SchedulerLogic;

namespace WindowsService
{
    public class ShedulerService
    {
        private readonly SchedulerSendMail _sendMail;
        public ShedulerService(SchedulerSendMail sendMail)
        {
            _sendMail = sendMail;
        }
        public void Start()
        {
            _sendMail.SendEmails().Start();
        }

        public void Stop()
        {
        }
    }
}