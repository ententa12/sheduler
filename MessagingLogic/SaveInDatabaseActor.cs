using System.Threading;
using Akka.Actor;
using CSVEmailModel;
using MailDatabaseInterface;

namespace MessagingLogic
{
    public class SaveInDatabaseActor : ReceiveActor
    {
        public SaveInDatabaseActor(IDatabaseContext<EmailPerson> context)
        {
            Receive<EmailsToSendRequest>(message => {
                context.SaveAll(message.EmailPersonToSend);
            });
        }
    }
}