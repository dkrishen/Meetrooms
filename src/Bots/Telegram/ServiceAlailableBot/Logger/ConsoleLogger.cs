using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAlailable.BOT.Logger
{
    internal class ConsoleLogger : ILogger
    {
        public void log(string message)
        {
            Console.WriteLine(DateTime.Now.ToString("[HH:mm:ss] ") + message);
        }
    }
}
