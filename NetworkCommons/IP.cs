using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkCommons
{
    public static class IP
    {
        public static string GetCurrentMachineIP()
        {
            string hostName = Dns.GetHostName();
            return Dns.GetHostEntry(hostName).AddressList[0].ToString();
        }
        
    }
}
