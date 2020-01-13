using System.Threading;
using CSVReaderLogic;
using MessagingLogic;
using RawRabbit;
using SchedulerLogic;

namespace WindowsService
{
    public class ShedulerService
    {
        private SchedulerSendMail _sendMail;
        private IBusClient _busClient;
        private ReadCsvHandler _readCsvHandler;
        private SaveInDatabaseHandler _saveInDatabaseHandler;
        private SendMailHandler _sendMailHandler;
        public ShedulerService(SchedulerSendMail sendMail, IBusClient busClient, ReadCsvHandler readCsvHandler,
            SaveInDatabaseHandler saveInDatabaseHandler, SendMailHandler sendMailHandler)
        {
            _sendMail = sendMail;
            _busClient = busClient;
            _readCsvHandler = readCsvHandler;
            _saveInDatabaseHandler = saveInDatabaseHandler;
            _sendMailHandler = sendMailHandler;
        }
        public void Start()
        {
            _busClient.SubscribeAsync<ReadCsvRequest>(async (msg, t) =>
            {
                await _readCsvHandler.HandleAsync(msg, CancellationToken.None);
            });
            _busClient.SubscribeAsync<EmailsToSendRequest>(async (msg, t) =>
            {
                await _saveInDatabaseHandler.HandleAsync(msg, CancellationToken.None);
            });
            _busClient.SubscribeAsync<EmailsToSendRequest>(async (msg, t) =>
            {
                await _sendMailHandler.HandleAsync(msg, CancellationToken.None);
            });
            _sendMail.SendEmails().GetAwaiter().GetResult();
        }

        public void Stop()
        {
        }
    }
}