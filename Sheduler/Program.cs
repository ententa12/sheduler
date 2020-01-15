using WindowsService;
using CSVEmailModel;
using CSVReaderInterface;
using CSVReaderLogic;
using EmailSenderInterface;
using FluentEmailSender;
using MailDatabase;
using MailDatabaseInterface;
using MessagingLogic;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using SchedulerLogic;

namespace StartUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = GetServicesCollection();
            new Configure(serviceProvider.GetService<ShedulerService>()).ConfigureService();
        }
        
        public static ServiceProvider GetServicesCollection()
        {
            return new ServiceCollection()
                .AddScoped<IDataReader<EmailPerson>, CsvEmailReader<EmailPerson>>()
                .AddScoped<IDatabaseContext<EmailPerson>, DatabaseLogic>()
                .AddScoped<IEmailSender<EmailPerson>, FluentSender>()
                .AddScoped<ShedulerService>()
                .AddScoped<SchedulerSendMail>()
                .AddScoped<SendJobFactory>()
                .AddSingleton<ILogger>((p) => LogManager.GetLogger("fileLogger"))
                .BuildServiceProvider();
        }
    }
}