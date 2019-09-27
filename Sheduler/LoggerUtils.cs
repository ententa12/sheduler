using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sheduler
{
    class LoggerUtils
    {
        public static Logger logger = LogManager.GetLogger("fileLogger");
    }
}
