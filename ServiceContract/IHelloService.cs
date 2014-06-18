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
        void ThrowError();

        [OperationContract]
        string RandomLength();
	}
}