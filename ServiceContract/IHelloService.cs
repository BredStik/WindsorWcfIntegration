using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        [OperationContract]
        string RandomLength();

        [OperationContract]

        [ServiceKnownType("GetKnownTypes", typeof(KnownTypesProvider))]
        object Handle(object request);
	}

    internal class KnownTypesProvider
    {
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            var contractAssembly = typeof(IRequest<>).Assembly;

            var requestTypes = (
                from type in contractAssembly.GetExportedTypes()
                where TypeIsRequestType(type)
                select type)
                .ToList();

            var resultTypes =
                from requestType in requestTypes
                select GetRequestResultType(requestType);

            return requestTypes.Union(resultTypes).ToArray();
        }

        private static bool TypeIsRequestType(Type type)
        {
            return GetRequestInterface(type) != null;
        }

        private static Type GetRequestResultType(Type requestType)
        {
            return GetRequestInterface(requestType).GetGenericArguments()[0];
        }

        private static Type GetRequestInterface(Type type)
        {
            return (
                from interfaceType in type.GetInterfaces()
                where interfaceType.IsGenericType
                where typeof(IRequest<>).IsAssignableFrom(
                    interfaceType.GetGenericTypeDefinition())
                select interfaceType)
                .SingleOrDefault();
        }
    }
}