using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using DIConfiguration;
using MailDatabaseInterface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingLogic
{
    public class SaveInDatabaseHandler : IHandler<EmailsToSend>
    {
        public async Task HandleAsync(EmailsToSend message, CancellationToken token)
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            await serviceProvider.GetService<IDatabaseContext<EmailPerson>>().SaveAll(message.EmailPersonToSend);
        }
    }
}