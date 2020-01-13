using WindowsService;
using CSVEmailModel;
using CSVReaderInterface;
using CSVReaderLogic;
using EmailSenderInterface;
using FluentEmailSender;
using MailDatabase;
using MailDatabaseInterface;
using MediatR;
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
            new Configure(GetServicesCollection().GetService<SchedulerSendMail>()).ConfigureService();
        }
        
        public static ServiceProvider GetServicesCollection()
        {
            return new ServiceCollection()
                .AddMediatR()
                .AddScoped<IDataReader<EmailPerson>, CsvEmailReader<EmailPerson>>()
                .AddScoped<IDatabaseContext<EmailPerson>, DatabaseLogic>()
                .AddScoped<IEmailSender<EmailPerson>, FluentSender>()
                .AddSingleton<ILogger>((p) => LogManager.GetLogger("fileLogger"))
                .AddSingleton<SchedulerSendMail>()
                .AddScoped<SendMailJob>()
                .AddScoped<SendJobFactory>()
                .AddScoped<ReadCsvHandler>()
                .AddScoped<SaveInDatabaseHandler>()
                .AddScoped<SendMailHandler>()
                .BuildServiceProvider();
        }
    }
}