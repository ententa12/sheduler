using Topshelf;

namespace WindowsService
{
    public class Configure
    {
        private ShedulerService _shedulerService;
        public Configure(ShedulerService shedulerService)
        {
            _shedulerService = shedulerService;
        }
        public void ConfigureService()
        {
            HostFactory
                .Run(configure =>
                {
                    configure.Service<ShedulerService>(service =>
                    {
                        service.ConstructUsing(s => _shedulerService);
                        service.WhenStarted(s => s.Start());
                        service.WhenStopped(s => s.Stop());
                    });
                    configure.RunAsLocalSystem();
                    configure.UseNLog();
                    configure.SetServiceName("Scheduler");
                    configure.SetDisplayName("Scheduler");
                    configure.SetDescription("Send emails");
                });
        }
    }
}