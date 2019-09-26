using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sheduler.Sheduler
{
    class SendMailJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            //await Console.Out.WriteLineAsync("Greetings from HelloJob!");
            await new Task(new Action(() =>
            {
                EmailSender.SendEmail();
                Console.Out.WriteLine("Greetings from HelloJob!");
            }
            ));
        }
    }
}
