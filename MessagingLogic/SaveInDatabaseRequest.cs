using CSVEmailModel;
using MediatR;

namespace MessagingLogic
{
    public class SaveInDatabaseRequest : INotification
    {
        public EmailPerson EmailPersonToSend { get; }
        public SaveInDatabaseRequest(EmailPerson emailPerson)
        {
            EmailPersonToSend = emailPerson;
        }
    }
}