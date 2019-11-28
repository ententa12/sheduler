using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using DIConfiguration;
using MailDatabaseInterface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingLogic
{
    public class SaveInDatabaseHandler : INotificationHandler<SaveInDatabaseRequest>
    {
        public async Task Handle(SaveInDatabaseRequest notification, CancellationToken cancellationToken)
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            await serviceProvider.GetService<IDatabaseContext<EmailPerson>>().Save(notification.EmailPersonToSend);
        }
    }
}