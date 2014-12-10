using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MyService;
using MyServiceContract;
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using Host;
using System.ServiceModel.Description;
using System.Collections.Generic;
using System.IdentityModel.Policy;

namespace Host
{
	public class Program
	{
		public Program()
		{
		}

        

		public static void Main()
		{
            //var helloServiceModel = new DefaultServiceModel(WcfEndpoint.BoundTo(new NetTcpBinding()).At("net.tcp://localhost:9101/hello"));//.AddExtensions(new GlobalExceptionHandlerBehaviour(typeof(GlobalExceptionHandler)));

            var throttlingBehavior = new ServiceThrottlingBehavior { MaxConcurrentCalls = Environment.ProcessorCount * 16, MaxConcurrentSessions = (Environment.ProcessorCount * 16) + (Environment.ProcessorCount * 100), MaxConcurrentInstances = Environment.ProcessorCount * 100 };
            

            var helloServiceModel = new DefaultServiceModel(WcfEndpoint.BoundTo(new NetTcpBinding()).At("net.tcp://localhost:9101/hello")).AddExtensions(new GlobalExceptionHandlerBehaviour(typeof(GlobalExceptionHandler)), throttlingBehavior );

            helloServiceModel.OnCreated(host => {
                host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
                host.Authorization.ExternalAuthorizationPolicies = new System.Collections.ObjectModel.ReadOnlyCollection<System.IdentityModel.Policy.IAuthorizationPolicy>(new List<IAuthorizationPolicy>() { new CustomAuthorizationPolicy() });


                //var od = host.Description.Endpoints[0].Contract.Operations.Find("Handle");
                //var serializerBehavior = od.Behaviors.Find<DataContractSerializerOperationBehavior>();
                
                //if (serializerBehavior == null)
                //{                    
                //    serializerBehavior = new DataContractSerializerOperationBehavior(od);
                //    od.Behaviors.Add(serializerBehavior);
                //}

                //serializerBehavior.DataContractResolver = new SharedTypeResolver();
            });



			var windsorContainer = new WindsorContainer().AddFacility<WcfFacility>();
            windsorContainer.Register(
                Component.For<LoggingCallContextInitializer>(),
                Component.For<LoggingBehavior>(),
                Component.For<IConsoleService>().ImplementedBy<ConsoleService>().AsWcfService(new DefaultServiceModel(WcfEndpoint.BoundTo(new NetTcpBinding()).At("net.tcp://localhost:9101/console"))),
                Component.For<IRequestHandlerService>().ImplementedBy<RequestHandlerService>().AsWcfService(new DefaultServiceModel(WcfEndpoint.BoundTo(new NetTcpBinding()).At("net.tcp://localhost:9101/requestHandler"))),
                Component.For<IHelloService>().ImplementedBy<HelloService>().AsWcfService(helloServiceModel)

                );

            var hostFactory = new DefaultServiceHostFactory(windsorContainer.Kernel);
            var helloHost = hostFactory.CreateServiceHost<IHelloService>();            
            var consoleHost = hostFactory.CreateServiceHost<IConsoleService>();
            var requestHandlerHost = hostFactory.CreateServiceHost<IRequestHandlerService>();

			try
			{
				Console.ReadLine();
			}
			finally
			{
                helloHost.Close();
                consoleHost.Close();
                requestHandlerHost.Close();
			}
		}
	}
}