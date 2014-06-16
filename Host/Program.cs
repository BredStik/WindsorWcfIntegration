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

namespace Host
{
	public class Program
	{
		public Program()
		{
		}

        

		public static void Main()
		{
			var windsorContainer = new WindsorContainer().AddFacility<WcfFacility>();
            windsorContainer.Register(
                Component.For<IConsoleService>().ImplementedBy<ConsoleService>().AsWcfService(new DefaultServiceModel(WcfEndpoint.BoundTo(new NetTcpBinding()).At("net.tcp://localhost:9101/console"))),
                Component.For<IHelloService>().ImplementedBy<HelloService>().AsWcfService(new DefaultServiceModel(WcfEndpoint.BoundTo(new NetTcpBinding()).At("net.tcp://localhost:9101/hello")))
                );

            var hostFactory = new DefaultServiceHostFactory(windsorContainer.Kernel);
            var helloHost = hostFactory.CreateServiceHost<IHelloService>();            
            var consoleHost = hostFactory.CreateServiceHost<IConsoleService>();

			try
			{
				Console.ReadLine();
			}
			finally
			{
                helloHost.Close();
                consoleHost.Close();
			}
            //Type type = typeof(HelloService);
            //Uri[] uri = new Uri[] { new Uri("net.tcp://localhost:9101") };
            //ServiceHost host = new ServiceHost(type, uri);
            //try
            //{
            //    NetTcpBinding binding = new NetTcpBinding();
            //    binding.Security.Mode = SecurityMode.Transport;
            //    binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            //    host.AddServiceEndpoint(typeof(IHelloService), binding, "");
            //    host.Open();
            //    Console.Write(string.Concat("Waiting for connection at ", host.BaseAddresses.First<Uri>().OriginalString));
            //    Console.ReadLine();
            //}
            //finally
            //{
            //    if (host != null)
            //    {
            //        ((IDisposable)host).Dispose();
            //    }
            //}
		}
	}
}