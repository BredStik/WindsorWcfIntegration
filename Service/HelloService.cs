using MyServiceContract;
using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading;

namespace MyService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)] 
	public class HelloService : IHelloService
	{
		public HelloService()
		{
		}

		//[PrincipalPermission(SecurityAction.Demand, Name="domain\\user")]
		public string SayHello(string name)
		{
            Thread.Sleep(3000);
			return string.Format("hello {0}!", Thread.CurrentPrincipal.Identity.Name);
		}
		
        public void ThrowError()
        {
            Thread.Sleep(3000);
            throw new ApplicationException("on purpose!");
        }

        public string RandomLength()
        {
            var rnd = new Random();
            var sleepTime = rnd.Next(2000, 10000);
            Thread.Sleep(sleepTime);

            return string.Format("Waited {0} milliseconds then returned.", sleepTime);
        }
	}
}