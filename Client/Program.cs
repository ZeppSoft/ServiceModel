﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProtoBuf.Meta;
using ServiceModel.Grpc.Client;
using ServiceModel.Grpc.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MessagePackMarshallerFactory = Shared.MessagePackMarshallerFactory;

namespace Client
{
    internal class Program
    {
        //private static readonly IClientFactory DefaultClientFactory = new ClientFactory(new ServiceModelGrpcClientOptions
        //{
        //    // set ProtobufMarshaller as default Marshaller
        //    MarshallerFactory = MessagePackMarshallerFactory.Default //JsonMarshallerFactory.Default//ProtobufMarshallerFactory.Default
        //});
        public static async Task Main(string[] args)
        {
            Thread.Sleep(3000);
            ICustomWareNET serviceWrapper = new ServiceWrapper();

            //var res = serviceWrapper.CalculateInterestAmount(new TestCWObject { ID = 101 }) ;
            // var obj = serviceWrapper.LoadObject(typeof(TestCWObject), "123");

            string id = "123";
           var obj = serviceWrapper.LoadObject<TestCWObject>("111");



            await Console.Out.WriteLineAsync("Press any key...");
            Console.ReadLine();

            //int SelfHostPort = 7000;
            //// register IPersonService proxy generated by ServiceModel.Grpc.DesignTime
            //DefaultClientFactory.AddClient<ILoanService>(options =>
            //{
            //    // setup ServiceModelGrpcClientOptions for this client
            //    // by default options contain values from default factory configuration
            //});


            //DefaultClientFactory.AddClient<ISimple>(options =>
            //{
            //    // setup ServiceModelGrpcClientOptions for this client
            //    // by default options contain values from default factory configuration
            //    options.MarshallerFactory = MessagePackMarshallerFactory.Default;
            //}) ;

            //DefaultClientFactory.AddClient<ICustomWareNET>(options =>
            //{
            //    // setup ServiceModelGrpcClientOptions for this client
            //    // by default options contain values from default factory configuration
            //    options.MarshallerFactory = MessagePackMarshallerFactory.Default;
            //});

            //Console.WriteLine("Call ServerSelfHost");
            //await Run(new Channel("localhost", SelfHostPort, ChannelCredentials.Insecure));

            if (Debugger.IsAttached)
            {
                Console.WriteLine("...");
                Console.ReadLine();
            }
        }
        private static async Task Run(ChannelBase channel)
        {
            //var loanManager = DefaultClientFactory.CreateClient<ILoanService>(channel);
            //var simpleService = DefaultClientFactory.CreateClient<ISimple>(channel);
            //var custom = DefaultClientFactory.CreateClient<ICustomWareNET>(channel);

            //var no = new TestCWObject();

            //  var t = custom.LoadObject<TestCWObject>("Hello");

            //var a = custom.CalculateInterestAmount(no);


            //var amount = loanManager.GetNextPaymentAmount("1.23", new DateTime(2023, 3, 1));

            //var os = new List<BaseObject> { new BaseObject { Value = "Test" } };

            //var sObject = simpleService.GetSimpleClass(1, "My Product", 2500, os);

            //var ob = simpleService.GetObject(typeof(BaseObject),new BaseObject {Value = "New object" });



            await Console.Out.WriteLineAsync("Press any key...");
             Console.ReadLine();

            /*
                        RuntimeTypeModel.Default.Add(typeof(Shared.Any.Container), false).SetSurrogate(typeof(Shared.Any));

                        var reply = await loanManager.TestContainer(
                            new Container { Value = "Hello world", Type = typeof(string) });
            */
            //var res =  cwnetService.Add(3, 2);
            //await Console.Out.WriteLineAsync(res.ToString());


            // var la = new LoanApplication() as Object;

            // var r = loanManager.LoadObject(typeof(LoanApplication), /*new Object()*/ null);

            // var k = loanManager.LoadObject2<LoanApplication>(new Object());

            // await Console.Out.WriteLineAsync(amount.ToString());
        }
    }
}