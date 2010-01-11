using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfServer
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in Web.config and in the associated .svc file.
    public class EchoService : IEchoService
    {
        public string Echo(string input)
        {
            Thread.Sleep(2000); // we're on the end of a bad network connection!
            return input;
        }
    }
}
