using MyServiceContract;
using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading;

namespace MyService
{
    //[GlobalExceptionHandlerBehaviour(typeof(GlobalExceptionHandler))]
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
            throw new FaultException<Exception>(new Exception("on purpose!"));
        }
	}
}