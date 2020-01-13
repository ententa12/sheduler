using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using EmailSenderInterface;
using MediatR;

namespace MessagingLogic
{
    public class SendMailHandler : IRequestHandler<SendMailRequest, Task>
    {
        private IEmailSender<EmailPerson> _emailSender;
        public SendMailHandler(IEmailSender<EmailPerson> emailSender)
        {
            _emailSender = emailSender;
        }

        public Task<Task> Handle(SendMailRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_emailSender.SendEmail(request.EmailPersonToSend));
        }
    }
}