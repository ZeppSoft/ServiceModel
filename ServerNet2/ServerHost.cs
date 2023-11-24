using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerNet2
{
    internal class ServerHost : IHostedService
    {
        //private readonly Server _server;
        //private int SelfHostPort = 7000;

        public ServerHost()
        {
            //_server = new Server
            //{
            //    Ports =
            //{
            //    new ServerPort("localhost", SelfHostPort, ServerCredentials.Insecure)
            //}
            //};


            IServiceCollection services = new ServiceCollection();

            //services.AddGrpc(options =>
            //{
            //    options.ResponseCompressionLevel = CompressionLevel.Optimal;
            //    // ...
            //});




            //services.AddSingleton(new ServerFilter1());
            //services.AddSingleton(new ServerFilter2());

            var serviceProvider = services.BuildServiceProvider();
        }

        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _server.Start();
        //    return Task.CompletedTask;
        //}

        //public Task StopAsync(CancellationToken cancellationToken) => _server.ShutdownAsync();
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
