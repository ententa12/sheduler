using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using MailDatabaseInterface;
using MediatR;

namespace MessagingLogic
{
    public class SaveInDatabaseHandler : INotificationHandler<SaveInDatabaseRequest>
    {
        private readonly IDatabaseContext<EmailPerson> _context;
        public SaveInDatabaseHandler(IDatabaseContext<EmailPerson> context)
        {
            _context = context;
        }
        public async Task Handle(SaveInDatabaseRequest notification, CancellationToken cancellationToken)
        {
            await _context.Save(notification.EmailPersonToSend);
        }
    }
}