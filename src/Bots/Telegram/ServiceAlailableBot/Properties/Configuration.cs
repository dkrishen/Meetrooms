using ServiceAlailable.BOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAlailable.BOT.Properties
{
    public class Configuration
    {
        public static LaunchProfile Development = new LaunchProfile(
            "5457143765:AAEuCDgNlGgksLapuBXoHog0b5fiJCH8RVg",
            "http://meetroom.ddns.net:83/",
            5000);

        public static LaunchProfile Docker = new LaunchProfile(
            "5457143765:AAEuCDgNlGgksLapuBXoHog0b5fiJCH8RVg",
            "http://meetroom.ddns.net:83/",
            30000);

        public static LaunchProfile Production = new LaunchProfile(
            "5457143765:AAEuCDgNlGgksLapuBXoHog0b5fiJCH8RVg",
            "http://meetroom.ddns.net:83/",
            300000);


    }
}
