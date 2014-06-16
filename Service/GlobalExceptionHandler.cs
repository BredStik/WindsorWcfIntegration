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
            var fault = new FaultException("An unexpected exception occured") { Source = ex.ToString() };

            var messageFault = fault.CreateMessageFault();
            msg = Message.CreateMessage(version, messageFault, fault.Action);
        }

        #endregion
    }
}
