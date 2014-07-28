using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    public class LoggingBehavior: IEndpointBehavior
    {
        private readonly ICallContextInitializer _loggingCallContextInitializer;

        public LoggingBehavior(LoggingCallContextInitializer loggingCallContextInitializer)
        {
            _loggingCallContextInitializer = loggingCallContextInitializer;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            foreach (var operation in endpointDispatcher.DispatchRuntime.Operations)
            {
                operation.CallContextInitializers.Add(_loggingCallContextInitializer);
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {           
        }
    }
}
