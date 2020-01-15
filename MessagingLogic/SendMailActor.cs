using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using CSVEmailModel;
using EmailSenderInterface;
using MailDatabaseInterface;
using NLog;

namespace MessagingLogic
{
    public class SendMailActor : ReceiveActor
    {
        public SendMailActor(IDatabaseContext<EmailPerson> database, ILogger logger, IEmailSender<EmailPerson> emailSender)
        {
            Receive<EmailsToSendRequest>(message => {
                var sendMails = message.EmailPersonToSend
                    .Where(e => !database.CheckIfExist(e))
                    .Select(e =>
                    {
                        logger.Debug("Save item in database with id: {0}", e.Id);
                        return emailSender.SendEmail(e);
                    });
                Task.WhenAll(sendMails);
            });
        }
    }
}