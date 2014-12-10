using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceContract
{
    public class MyRequest: IRequest<MyResponse>
    {
        public string Parameter { get; set; }
    }
}
