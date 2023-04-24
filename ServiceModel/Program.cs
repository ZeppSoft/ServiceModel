using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel
{
    internal class Program
    {
        public static Task Main(string[] args)
        {
            return Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(services => services.AddHostedService<ServerHost>())
            .Build()
            .RunAsync();
        }
    }
}
