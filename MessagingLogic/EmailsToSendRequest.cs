using System.Collections.Generic;
using CSVEmailModel;

namespace MessagingLogic
{
    public class EmailsToSendRequest : IMessage
    {
        public EmailPerson EmailPersonToSend { get; set; }
        public EmailsToSendRequest(EmailPerson emailPerson)
        {
            EmailPersonToSend = emailPerson;
        }
    }
}