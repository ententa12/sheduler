using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using DIConfiguration;
using EmailSenderInterface;
using MailDatabaseInterface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace MessagingLogic
{
    public class SendMailHandler : IHandler<EmailsToSendRequest>
    {
        public async Task HandleAsync(EmailsToSendRequest message, CancellationToken token)
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            
            var sendMails = message.EmailPersonToSend
                .Where(e => !serviceProvider.GetService<IDatabaseContext<EmailPerson>>().CheckIfExist(e))
                .Select(e =>
                {
                    serviceProvider.GetService<ILogger>().Debug("Save item in database with id: {0}", e.Id);
                    return serviceProvider.GetService<IEmailSender<EmailPerson>>().SendEmail(e);
                });
            await Task.WhenAll(sendMails);
        }
    }
}