using Castle.Facilities.WcfIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    public static class ServiceFactoryExtensions
    {
        public static ServiceHostBase CreateServiceHost<TService>(this DefaultServiceHostFactory factory)
        {
            return factory.CreateServiceHost(typeof(TService).AssemblyQualifiedName, new Uri[0]);
        }
    }
}
