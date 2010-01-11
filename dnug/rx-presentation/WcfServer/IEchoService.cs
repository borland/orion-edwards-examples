using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServer
{
    [ServiceContract]
    public interface IEchoService
    {
        [OperationContract]
        string Echo(string input);
    }
}
