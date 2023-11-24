using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerNet1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            return Host
           .CreateDefaultBuilder(args)
           .ConfigureServices(services => services.AddHostedService<ServerHost>())
           .Build()
           .RunAsync();
        }
    }
}
