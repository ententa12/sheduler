using NLog;

namespace NLogger
{
    public class Logger
    {
        public ILogger GetLogger()
        {
            return LogManager.GetLogger("fileLogger");
        }
    }
}