using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    public class LoggingCallContextInitializer: ICallContextInitializer
    {
        public void AfterInvoke(object correlationState)
        {
            Console.WriteLine("After invoke!");
        }

        public object BeforeInvoke(System.ServiceModel.InstanceContext instanceContext, System.ServiceModel.IClientChannel channel, System.ServiceModel.Channels.Message message)
        {
            Console.WriteLine("Before invoke!");
            return null;
        }
    }
}
