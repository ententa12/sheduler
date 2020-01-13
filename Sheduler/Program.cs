using WindowsService;
using CSVEmailModel;
using CSVReaderInterface;
using CSVReaderLogic;
using EmailSenderInterface;
using FluentEmailSender;
using MailDatabase;
using MailDatabaseInterface;
using MessagingLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;
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
            var options = new RawRabbitConfiguration();
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            config.GetSection("rabbitmq").Bind(options);
            var client = BusClientFactory.CreateDefault(options);
            return new ServiceCollection()
                .AddSingleton<IBusClient>(_ => client)
                .AddScoped<IDataReader<EmailPerson>, CsvEmailReader<EmailPerson>>()
                .AddScoped<IDatabaseContext<EmailPerson>, DatabaseLogic>()
                .AddScoped<IEmailSender<EmailPerson>, FluentSender>()
                .AddScoped<ShedulerService>()
                .AddScoped<ReadCsvHandler>()
                .AddScoped<SaveInDatabaseHandler>()
                .AddScoped<SendMailHandler>()
                .AddScoped<SchedulerSendMail>()
                .AddScoped<SendJobFactory>()
                .AddSingleton<ILogger>((p) => LogManager.GetLogger("fileLogger"))
                .BuildServiceProvider();
        }
    }
}