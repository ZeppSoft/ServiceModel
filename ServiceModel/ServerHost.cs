using Grpc.Core;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Meta;
using ServiceModel.Grpc.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MessagePackMarshallerFactory = Shared.MessagePackMarshallerFactory;

namespace ServiceModel
{
    internal class ServerHost : IHostedService
    {
        private readonly Server _server;
        private int SelfHostPort = 7000;

        public ServerHost()
        {
            _server = new Server
            {
                Ports =
            {
                new ServerPort("localhost", SelfHostPort, ServerCredentials.Insecure)
            }
            };

            RuntimeTypeModel.Default.Add(typeof(Any.Container), false).SetSurrogate(typeof(Any));


            _server.Services.AddServiceModelSingleton(
                new LoanService(),
                options =>
                {
                    // set ProtobufMarshaller as default Marshaller
                    options.MarshallerFactory = JsonMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
                });

            _server.Services.AddServiceModelSingleton(
                new SimpleService(),
                options =>
                {
                    // set ProtobufMarshaller as default Marshaller
                    options.MarshallerFactory = MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
                });

            _server.Services.AddServiceModelSingleton(
                new CustomWareService(),
                options =>
                {
                    // set ProtobufMarshaller as default Marshaller
                    options.MarshallerFactory = MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
                });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _server.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => _server.ShutdownAsync();
    }
}
