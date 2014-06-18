using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class GlobalExceptionHandler : IErrorHandler
    {
        #region IErrorHandler Members

        public bool HandleError(Exception ex)
        {
            // Do some logging here
            //Logger.LogException(ex);
            return true;
        }

        public void ProvideFault(Exception ex, MessageVersion version, ref Message msg)
        {
            //var genericFaultException = typeof(FaultException<>).MakeGenericType(ex.GetType());
            //var customFault = Activator.CreateInstance(genericFaultException, ex) as FaultException;
            var customFault = new FaultException<ApplicationException>((ApplicationException)ex, "no reason");
            
            var fault = new FaultException("An unexpected exception occured") { Source = ex.ToString() };

            var messageFault = customFault.CreateMessageFault();
            msg = Message.CreateMessage(version, messageFault, customFault.Action);
        }

        #endregion
    }

    public class Test
    {
        public string Name { get; set; }
    }
}
