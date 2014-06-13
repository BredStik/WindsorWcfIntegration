using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MyServiceContract;
using System;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Client
{
	public class Program
	{
		public Program()
		{
		}

		public static void Main()
		{
            
			AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            IWindsorContainer container = null;

            try
            {
                container = GetContainer();

                var service = container.Resolve<IHelloService>();
                Console.WriteLine(service.SayHello("world"));
                container.Release(service);


                var factory = new ChannelFactory<IHelloService>(new NetTcpBinding());
                try
                {
                    IHelloService channel = factory.CreateChannel(new EndpointAddress("net.tcp://localhost:9101/hello"));
                    using (var clientChannel = channel as IClientChannel)
                    {
                        channel.SayHello("world");
                        clientChannel.Close();
                    }
                }
                finally
                {
                    factory.Close();
                }
            }
            finally
            {
                if (container != null)
                {
                    container.Dispose();
                }
            }
			Console.ReadLine();
		}

        private static WindsorContainer GetContainer()
        {
            var container = new WindsorContainer();
            container.Kernel.AddFacility<WcfFacility>();
            container.Register(Component.For<IHelloService>()
                                   .AsWcfClient(new DefaultClientModel
                                   {
                                       Endpoint = WcfEndpoint.BoundTo(new NetTcpBinding())
                                           .At("net.tcp://localhost:9101/hello")
                                   }));
            return container;
        }
	}
}