using MyServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class ConsoleService: IConsoleService
    {

        public void ConsoleWrite(string message)
        {
            Console.WriteLine(message);
        }
    }
}
