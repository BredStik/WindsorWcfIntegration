using MyServiceContract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class RequestHandlerService: IRequestHandlerService
    {
        public string Handle(string serializedRequest, string type)
        {
            var requestType = Type.GetType(type);

            var request = Newtonsoft.Json.JsonConvert.DeserializeObject(serializedRequest, requestType);

            //todo: handle request

            return JsonConvert.SerializeObject(new MyResponse{Message = "test"});
        }
    }
}
