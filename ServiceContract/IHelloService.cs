using System;
using System.ServiceModel;

namespace MyServiceContract
{
	[ServiceContract]
	public interface IHelloService
	{
		[OperationContract]
		string SayHello(string name);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ThrowError();
	}
}