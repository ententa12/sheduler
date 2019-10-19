using CSVEmailModel;
using CSVReaderInterface;
using CSVReaderLogic;
using EmailSenderInterface;
using EmailSenderLogic;
using MailDatabase;
using MailDatabaseInterface;
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
            Bind<IEmailSender<EmailPerson>>().To<EmailSender>();
            Bind<ILogger>().ToMethod((p) => LogManager.GetLogger("fileLogger"));
        }
    }
}