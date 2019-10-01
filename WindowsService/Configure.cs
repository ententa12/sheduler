using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;

namespace WindowsService
{
    public class Configure
    {
        public void ConfigureService()
        {
            HostFactory
                .Run(configure => {
                    configure.Service<ShedulerService>(service =>
                    {
                        service.ConstructUsing(s => new ShedulerService());
                        service.WhenStarted(s => s.Start());
                        service.WhenStopped(s => s.Stop());
                    });
                    configure.RunAsLocalSystem();
                    configure.UseNLog();
                    configure.SetServiceName("Sheduler");
                    configure.SetDisplayName("Sheduler");
                    configure.SetDescription("Send emails");
                });
        }
    }
}
