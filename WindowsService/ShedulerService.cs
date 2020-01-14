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
        private readonly IBusClient _busClient;
        private readonly ReadCsvHandler _readCsvHandler;
        private readonly SaveInDatabaseHandler _saveInDatabaseHandler;
        private readonly SendMailHandler _sendMailHandler;
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
            _sendMail.SendEmails().Start();
        }

        public void Stop()
        {
        }
    }
}