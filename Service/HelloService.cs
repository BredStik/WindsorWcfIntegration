using MyServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            throw new FaultException<Exception>(new Exception("on purpose!"));
        }

        public string RandomLength()
        {
            var rnd = new Random();
            var sleepTime = rnd.Next(2000, 10000);
            //Thread.Sleep(sleepTime);

            return string.Format("Waited {0} milliseconds then returned.", sleepTime);
        }

        public object Handle(object request)
        {
            var requestInterfaceType = request.GetType().GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRequest<>));
            var responseType = requestInterfaceType.GetGenericArguments().First();

            if(request is IRequest<MyResponse>)
            {
                return new MyResponse { Message = "hello", Other = new OtherComplexObject { Id = 1, Date = DateTime.Now } };
            }

            return Activator.CreateInstance(responseType);
        }


        //public TResponse Handle<TRequest, TResponse>(TRequest request)
        //{
        //    //var requestInterfaceType = request.GetType().GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRequest<>));
        //    //var responseType = requestInterfaceType.GetGenericArguments().First();

        //    return default(TResponse);
        //}

        
    }
}