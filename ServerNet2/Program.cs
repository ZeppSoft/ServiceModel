using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerNet2
{
    internal class Program
    {
        static Task Main(string[] args)
        {
            return Host
           .CreateDefaultBuilder(args)
           .ConfigureServices(services => services.AddHostedService<ServerHost>())
           .Build()
           .RunAsync();
        }
    }
}
