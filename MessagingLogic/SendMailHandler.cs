using System;
using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using DIConfiguration;
using EmailSenderInterface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingLogic
{
    public class SendMailHandler : IRequestHandler<SendMailRequest, Task>
    {
        public Task<Task> Handle(SendMailRequest request, CancellationToken cancellationToken)
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            return Task.FromResult(serviceProvider.GetService<IEmailSender<EmailPerson>>().SendEmail(request.EmailPersonToSend));
        }
    }
}