using System.Linq;
using System.Net;

namespace NetworkCommons
{
    public static class IP
    {
        public static string GetCurrentMachineIP()
        {
            string hostName = Dns.GetHostName();
            return Dns.GetHostByName(hostName).AddressList.First().ToString();
        }

    }
}
