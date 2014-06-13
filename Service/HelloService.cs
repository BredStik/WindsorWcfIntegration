using MyServiceContract;
using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace MyService
{
	public class HelloService : IHelloService
	{
		public HelloService()
		{
		}

		//[PrincipalPermission(SecurityAction.Demand, Name="domain\\user")]
		public string SayHello(string name)
		{
			return string.Format("hello {0}!", Thread.CurrentPrincipal.Identity.Name);
		}
	}
}