﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceContract
{
    public class MyResponse
    {
        public string Message { get; set; }
        public OtherComplexObject Other { get; set; }
    }
}
