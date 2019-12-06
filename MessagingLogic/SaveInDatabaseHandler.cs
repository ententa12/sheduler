using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using DIConfiguration;
using MailDatabaseInterface;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingLogic
{
    public class SaveInDatabaseHandler : IHandler<EmailsToSendRequest>
    {
        public async Task HandleAsync(EmailsToSendRequest message, CancellationToken token)
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            await serviceProvider.GetService<IDatabaseContext<EmailPerson>>().SaveAll(message.EmailPersonToSend);
        }
    }
}