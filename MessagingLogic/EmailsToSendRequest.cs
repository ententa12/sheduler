using System.Collections.Generic;
using CSVEmailModel;

namespace MessagingLogic
{
    public class EmailsToSendRequest : IMessage
    {
        public List<EmailPerson> EmailPersonToSend { get; }
        public EmailsToSendRequest(List<EmailPerson> emailPerson)
        {
            EmailPersonToSend = emailPerson;
        }
    }
}