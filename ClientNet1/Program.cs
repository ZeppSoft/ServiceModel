using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Grpc.Net.Client.Web;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using MessagePack;
using ServiceModel.Grpc.Client;
using ServiceModel.Grpc.Configuration;
using Shared;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MessagePackMarshallerFactory = ServiceModel.Grpc.Configuration.MessagePackMarshallerFactory;

namespace ClientNet1
{
    internal class Program
    {
        private static readonly IClientFactory DefaultClientFactory = new ClientFactory(new ServiceModelGrpcClientOptions
        {
            // set ProtobufMarshaller as default Marshaller
            MarshallerFactory = MessagePackMarshallerFactory.Default //JsonMarshallerFactory.Default//ProtobufMarshallerFactory.Default

        });
        static void Main(string[] args)
        {
            Thread.Sleep(3000);


            var defaultMethodConfig = new MethodConfig
            {
                Names = { MethodName.Default },
                HedgingPolicy = new HedgingPolicy
                {
                    MaxAttempts = 5,
                    NonFatalStatusCodes = { StatusCode.Unavailable }
                }
            };

            //var channel = GrpcChannel.ForAddress("https://localhost:7000");

            var channel = GrpcChannel.ForAddress("http://localhost:5195", new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler()),
                ServiceConfig = new ServiceConfig { MethodConfigs = { defaultMethodConfig } }
            });




            IReadOnlyList<IMessagePackFormatter> formatters =
               new List<IMessagePackFormatter> { MessagePack.Formatters.TypelessFormatter.Instance,  new TestPersonFormatter() };



            IReadOnlyList<IFormatterResolver> resolvers =
                new List<IFormatterResolver> { ContractlessStandardResolver.Instance, StandardResolver.Instance, TypelessContractlessStandardResolver.Instance };




            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
                            formatters,
                        resolvers);


            var opt = MessagePackSerializerOptions.Standard.WithResolver(resolver);













            DefaultClientFactory.AddClient<ISomeManager>(options =>
            {
                options.MarshallerFactory = new MessagePackMarshallerFactory(opt);//MessagePackMarshallerFactory.Default;

            });

            var _sm = DefaultClientFactory.CreateClient<ISomeManager>(channel);


            var person = _sm.GetTestPerson();

            var pname = person as TestPerson;

            var name = pname.LastName;

            Console.ReadKey();
        }
    }
}
