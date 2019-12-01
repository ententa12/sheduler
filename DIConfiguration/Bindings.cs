using System.Configuration;
using CSVEmailModel;
using CSVReaderInterface;
using CSVReaderLogic;
using EmailSenderInterface;
using FluentEmailSender;
using MailDatabase;
using MailDatabaseInterface;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;

namespace DIConfiguration
{
    public class Bindings
    {
        public ServiceProvider GetServicesCollection()
        {
            var options = new RawRabbitConfiguration();
            ((IConfigurationSection)ConfigurationManager.GetSection("rabbitmq")).Bind(options);
            var client = BusClientFactory.CreateDefault(options);
            return new ServiceCollection()
                .AddSingleton<IBusClient>(_ => client)
                .AddScoped<IDataReader<EmailPerson>, CsvEmailReader<EmailPerson>>()
                .AddScoped<IDatabaseContext<EmailPerson>, DatabaseLogic>()
                .AddScoped<IEmailSender<EmailPerson>, FluentSender>()
                .AddSingleton<ILogger>((p) => LogManager.GetLogger("fileLogger"))
                .BuildServiceProvider();
        }
    }
}