using CSVEmailModel;
using CSVReaderInterface;
using CSVReaderLogic;
using EmailSenderInterface;
using FluentEmailSender;
using MailDatabase;
using MailDatabaseInterface;
using MediatR.Ninject;
using Ninject;
using Ninject.Modules;
using NLog;

namespace DIConfiguration
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataReader<EmailPerson>>().To<CsvEmailReader<EmailPerson>>();
            Bind<IDatabaseContext<EmailPerson>>().To<DatabaseLogic>();
            Bind<IEmailSender<EmailPerson>>().To<FluentSender>();
            Bind<ILogger>().ToMethod((p) => LogManager.GetLogger("fileLogger"));
        }
    }
}