using System;

namespace Sheduler
{
    class Program
    {
        static void Main(string[] args)
        { 
            EmailSender.SendEmail();
            ConfigureService.Configure();
        }
    }
}
