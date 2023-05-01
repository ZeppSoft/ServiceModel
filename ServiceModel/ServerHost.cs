using Grpc.Core;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using MessagePack;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Meta;
using ServiceModel.Grpc.Configuration;
using ServiceModel.Services;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MessagePackMarshallerFactory = ServiceModel.Grpc.Configuration.MessagePackMarshallerFactory;
//using MessagePackMarshallerFactory = Shared.MessagePackMarshallerFactory;

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

            IReadOnlyList<IMessagePackFormatter> formatters =
              new List<IMessagePackFormatter> { MessagePack.Formatters.TypelessFormatter.Instance, new CWObjectFormatter(), new IListFormatter() };



            IReadOnlyList<IFormatterResolver> resolvers =
                new List<IFormatterResolver> { ContractlessStandardResolver.Instance, StandardResolver.Instance, TypelessContractlessStandardResolver.Instance };




            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
                            formatters,
                        resolvers);


            var opt = MessagePackSerializerOptions.Standard.WithResolver(resolver);


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
                    options.MarshallerFactory = new MessagePackMarshallerFactory(opt);//MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
                });


            _server.Services.AddServiceModelSingleton(
              new SomeManager(),
              options =>
              {
                  //options.
                  // set ProtobufMarshaller as default Marshaller
                  options.MarshallerFactory = new MessagePackMarshallerFactory(opt);
                  //options.MarshallerFactory = MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
              });

            _server.Services.AddServiceModelSingleton(
                new CustomWareService(new SomeManager()),
                options =>
                {
                    //options.
                    // set ProtobufMarshaller as default Marshaller
                    options.MarshallerFactory = new MessagePackMarshallerFactory(opt);
                    //options.MarshallerFactory = MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
                });
            _server.Services.AddServiceModelSingleton(
                new CalculatorNullableInterfaceService(),
                options =>
                {
                    //options.
                    // set ProtobufMarshaller as default Marshaller
                    options.MarshallerFactory = new MessagePackMarshallerFactory(opt);
                    //options.MarshallerFactory = MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
                });

           

            _server.Services.Add(Greeter.BindService(new GreeterService()));

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _server.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => _server.ShutdownAsync();
    }
}
