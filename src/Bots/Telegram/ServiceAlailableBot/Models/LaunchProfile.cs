using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ServiceAlailable.BOT.Models
{
    public class LaunchProfile
    {
        public string token;
        public string serviceAddress;
        public int ObserveTimespan;

        public LaunchProfile(string token, string serviceAddress, int observeTimespan)
        {
            this.token = token;
            this.serviceAddress = serviceAddress;
            ObserveTimespan = observeTimespan;
        }
    }
}
