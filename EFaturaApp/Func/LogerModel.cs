using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFaturaApp.Func
{
    class LogerModel
    {
        public DateTime Timestamp { get; set; }
        public string Loglevel { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
        public string Message { get; set; }
    }
}
