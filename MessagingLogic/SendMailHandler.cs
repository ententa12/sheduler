using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using EmailSenderInterface;
using MailDatabaseInterface;
using NLog;

namespace MessagingLogic
{
    public class SendMailHandler : IHandler<EmailsToSendRequest>
    {
        private readonly IDatabaseContext<EmailPerson> _database;
        private readonly ILogger _logger;
        private readonly IEmailSender<EmailPerson> _emailSender;

        public SendMailHandler(IDatabaseContext<EmailPerson> database, ILogger logger, IEmailSender<EmailPerson> emailSender)
        {
            _database = database;
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task HandleAsync(EmailsToSendRequest message, CancellationToken token)
        {
            var emailPerson = message.EmailPersonToSend;
            if (!_database.CheckIfExist(emailPerson))
            {
                _logger.Debug("Send email to person with id: {0}", emailPerson.Id);
                await _emailSender.SendEmail(emailPerson);
            }
        }
    }
}