using NLog;

namespace Logger
{
    public class LoggerP
    {
        public ILogger GetLogger()
        {
            return LogManager.GetLogger("fileLogger");
        }
    }
}