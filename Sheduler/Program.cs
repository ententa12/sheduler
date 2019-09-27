using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Sheduler.EmailReader;
using Sheduler.Model;
using Sheduler.Sheduler;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Sheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureService.Configure();
        }
    }
}

