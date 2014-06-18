using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MyServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            IWindsorContainer container = GetContainer();


            Form1 form = null;

            try
            {
                form = container.Resolve<Form1>();

                Application.Run(form);
            }
            finally
            {
                if (form != null)
                {
                    container.Release(form);
                }

                container.Dispose();
            }
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
            container.Register(Classes.FromThisAssembly().BasedOn(typeof(Form)).LifestyleTransient());
            return container;
        }
    }
}
