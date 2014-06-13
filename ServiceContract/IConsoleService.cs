using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceContract
{
    [ServiceContract]
    public interface IConsoleService
    {
        [OperationContract]
        void ConsoleWrite(string message);
    }
}
