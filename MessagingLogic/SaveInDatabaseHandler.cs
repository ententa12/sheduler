using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using MailDatabaseInterface;

namespace MessagingLogic
{
    public class SaveInDatabaseHandler : IHandler<EmailsToSendRequest>
    {
        private readonly IDatabaseContext<EmailPerson> _busClient;

        public SaveInDatabaseHandler(IDatabaseContext<EmailPerson> busClient)
        {
            _busClient = busClient;
        }

        public async Task HandleAsync(EmailsToSendRequest message, CancellationToken token)
        {
            await _busClient.SaveAll(message.EmailPersonToSend);
        }
    }
}