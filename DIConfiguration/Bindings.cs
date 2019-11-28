using CSVEmailModel;
using CSVReaderInterface;
using CSVReaderLogic;
using EmailSenderInterface;
using FluentEmailSender;
using MailDatabase;
using MailDatabaseInterface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace DIConfiguration
{
    public class Bindings
    {
        public ServiceProvider GetServicesCollection()
        {
            return new ServiceCollection()
                .AddMediatR()
                .AddScoped<IDataReader<EmailPerson>, CsvEmailReader<EmailPerson>>()
                .AddScoped<IDatabaseContext<EmailPerson>, DatabaseLogic>()
                .AddScoped<IEmailSender<EmailPerson>, FluentSender>()
                .AddSingleton<ILogger>((p) => LogManager.GetLogger("fileLogger"))
                .BuildServiceProvider();
        }
    }
}