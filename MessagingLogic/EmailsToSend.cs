using System.Collections.Generic;
using System.Threading.Tasks;
using CSVEmailModel;
using MediatR;

namespace MessagingLogic
{
    public class EmailsToSend : IMessage
    {
        public List<EmailPerson> EmailPersonToSend { get; }
        public EmailsToSend(List<EmailPerson> emailPerson)
        {
            EmailPersonToSend = emailPerson;
        }
    }
}