using System.Collections.Generic;
using System.Threading.Tasks;
using CSVEmailModel;
using MediatR;

namespace MessagingLogic
{
    public class SendMailRequest : IRequest<Task>
    {
        public EmailPerson EmailPersonToSend { get; }
        public SendMailRequest(EmailPerson emailPerson)
        {
            EmailPersonToSend = emailPerson;
        }
    }
}