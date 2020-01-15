using System.Collections.Generic;
using CSVEmailModel;

namespace MessagingLogic
{
    public class EmailsToSendRequest
    {
        public List<EmailPerson> EmailPersonToSend { get; set; }
        public EmailsToSendRequest(List<EmailPerson> emailPerson)
        {
            EmailPersonToSend = emailPerson;
        }
    }
}