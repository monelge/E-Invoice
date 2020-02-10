using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace EFaturaApp.Func
{
    public static class LoggerClass
    {
        public static Logger  logger = LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();

        private static string error;

        public static string ERROR
        {
            set
            {
                logger = LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();
                logger.Error(value);
            }
        }

    }
}
