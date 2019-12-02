using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using CSVReaderLogic;
using DIConfiguration;
using MailDatabaseInterface;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using RawRabbit;

namespace SchedulerLogic
{
    public class HandlerJob : IJob
    {
        private readonly IBusClient _client;

        public HandlerJob()
        {
            var serviceProvider = new Bindings().GetServicesCollection();
            _client = serviceProvider.GetService<IBusClient>();
        }
        
        public Task Execute(IJobExecutionContext context)
        {
            _client
                .SubscribeAsync<ReadCsvRequest>(async (msg, t) =>
                {
                    await ReadCsvHandler.HandleAsync(msg, CancellationToken.None);
                });
            return Task.CompletedTask;
        }
    }
}