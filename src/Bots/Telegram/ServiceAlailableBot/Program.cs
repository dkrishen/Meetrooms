using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceAlailable.BOT.Logger;
using ServiceAlailable.BOT.Models;
using ServiceAlailable.BOT.Properties;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MRA.ServiceAvailable
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // temp hardcode
            var config = Configuration.Development;

            var bot = new ServiceAvailableBot(config.token, config.serviceAddress, config.ObserveTimespan);
            bot.AddLoggger(new ConsoleLogger());

            bot.Run();
            await bot.StartObserveAsync();

            Console.ReadLine();
        }

    }
}