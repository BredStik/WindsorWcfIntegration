using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceContract
{
    [ServiceContract]
    public interface IRequestHandlerService
    {
        [OperationContract]
        string Handle(string serializedRequest, string type);
    }
}
