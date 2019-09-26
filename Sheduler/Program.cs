using NLog;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Sheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            //EmailSender.SendEmail();
            //ConfigureService.Configure();

            try
            {
                int zero = 0;
                int result = 5 / zero;
            }
            catch (DivideByZeroException ex)
            {
                // get a Logger object and log exception here using NLog. 
                // this will use the "fileLogger" logger from our NLog.config file
                Logger logger = LogManager.GetLogger("fileLogger");

                // add custom message and pass in the exception
                logger.Error(ex, "Whoops!");
            }
        }
    }
}

