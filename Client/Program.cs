﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using MessagePack;
using ProtoBuf.Meta;
using ServiceModel.Grpc.Client;
using ServiceModel.Grpc.Configuration;
using Shared;
using Shared.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MessagePackMarshallerFactory = ServiceModel.Grpc.Configuration.MessagePackMarshallerFactory;
using ServiceModel.Grpc.Filters;
//using MessagePackMarshallerFactory = Shared.MessagePackMarshallerFactory;

namespace Client
{

    internal class ClientFilter1 : IClientFilter
    {
        public void Invoke(IClientFilterContext context, Action next)
        {
            try
            {
                // invoke all other filters in the stack and do service call
                next();
            }
            catch (Exception ex)
            {
                //OnError(context, logger, ex);
                throw;
            }

        }

        public ValueTask InvokeAsync(IClientFilterContext context, Func<ValueTask> next)
        {
            throw new NotImplementedException();
        }
    }
    internal class ClientFilter2 : IClientFilter
    {
        public void Invoke(IClientFilterContext context, Action next)
        {
            try
            {
                OnRequest(context);

                // invoke all other filters in the stack and do service call
                next();
            }
            catch (Exception ex)
            {
                OnError(context, ex);
                throw;
            }

            OnResponse(context);
        }

        public ValueTask InvokeAsync(IClientFilterContext context, Func<ValueTask> next)
        {
            throw new NotImplementedException();
        }


        public  void OnRequest(IClientFilterContext context)
        {
            // log input
            //LogBegin(logger, context.Request);


            if (context.Request.Count > 0)
            {

                // var ar = context.Request.ToArray();

                //foreach (var item in context.Request)
                for (int i = 0; i < context.Request.Count; i++)
                {
                    try
                    {
                        var item = context.Request[i] as ICWObject;
                        if (item != null)
                        {
                            var ser = PersonSerializeHelper.DoConvert(item);
                            context.Response[i] = ser;

                           
                        }


                        //if (ser != null)
                        //{
                        //    var person = PersonSerializeHelper.DoDeConvert(ser);
                        //    ar[i] = new KeyValuePair<string, object>(ar[i].Key, person);
                        //}
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }


        }

        public void OnResponse(IClientFilterContext context)
        {
            if (context.Response.IsProvided)
            {
                if (context.Response.Count > 0)
                {

                    for (int i = 0; i < context.Response.Count; i++)
                    {
                        try
                        {
                            var item = context.Response[i] as ICWObject;
                            if (item != null)
                            {
                                var person = PersonSerializeHelper.DoDeConvert(item);
                                context.Response[i] = person;
                            }


                            //if (ser != null)
                            //{
                            //    var person = PersonSerializeHelper.DoDeConvert(ser);
                            //    ar[i] = new KeyValuePair<string, object>(ar[i].Key, person);
                            //}
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }

                }
            }
        }

        public void OnError(IClientFilterContext context,Exception error)
        {
            // log exception
           // logger.LogError("error {0} failed: {1}", context.ContractMethodInfo.Name, error);
        }

    }



    public class BaseTest
    {
        public int MyProperty { get; set; }
    }
    internal class Program
    {
        public static string GetCity((string first, string second, int value, string third) cityName)
        {
           
            return cityName.first;
        }
        private static readonly IClientFactory DefaultClientFactory = new ClientFactory(new ServiceModelGrpcClientOptions
        {
            // set ProtobufMarshaller as default Marshaller
            MarshallerFactory = MessagePackMarshallerFactory.Default //JsonMarshallerFactory.Default//ProtobufMarshallerFactory.Default
            
        });
        public static async Task Main(string[] args)
        {
            Thread.Sleep(3000);
            ICustomWareNET serviceWrapper = new ServiceWrapper();

            int SelfHostPort = 7000;

            IReadOnlyList<IMessagePackFormatter> formatters =
               new List<IMessagePackFormatter> { MessagePack.Formatters.TypelessFormatter.Instance, /*new CWObjectFormatter(), new IListFormatter(),*/ new TestPersonSerializedFormatter() };



            IReadOnlyList<IFormatterResolver> resolvers =
                new List<IFormatterResolver> { ContractlessStandardResolver.Instance, StandardResolver.Instance, TypelessContractlessStandardResolver.Instance };




            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
                            formatters,
                        resolvers);


            var opt = MessagePackSerializerOptions.Standard.WithResolver(resolver);



            DefaultClientFactory.AddClient<ICalculator<ICWObject>>(options =>
            {
                // setup ServiceModelGrpcClientOptions for this client
                // by default options contain values from default factory configuration
                options.MarshallerFactory = MessagePackMarshallerFactory.Default;
            });

            Console.WriteLine("Call ServerSelfHost");
            var channel = new Channel("localhost", SelfHostPort, ChannelCredentials.Insecure);

            DefaultClientFactory.AddClient<ICustomWareNET>(options =>
            {
                options.MarshallerFactory = MessagePackMarshallerFactory.Default;
            });

            var _service = DefaultClientFactory.CreateClient<ICustomWareNET>(channel);




            DefaultClientFactory.AddClient<ISomeManager>(options =>
            {
                // setup ServiceModelGrpcClientOptions for this client
                // by default options contain values from default factory configuration
                options.MarshallerFactory = new MessagePackMarshallerFactory(opt);//  = MessagePackMarshallerFactory.Default.;
                //options.Filters.Add(1,new ClientFilter1());
                options.Filters.Add(2, new ClientFilter2());

            });


            var _sm = DefaultClientFactory.CreateClient<ISomeManager>(channel);


            var person = _sm.GetTestPerson();
            var penalties = _sm.GetPenalty("123");

            var nPerson = person as TestPerson;

            var name = nPerson.LastName;


            //var co = _sm.GetComplexObject();

            //var t = co.IntProp;


            //var obk = _sm.GetCWObject("12223");


            //var amounts = _sm.GetAmounts("123");

            //List<RepaymentAmount> amounts = _sm.GetAmounts("123") as List<RepaymentAmount>;

            // List<PenaltyAmount> pen = _sm.GetPenalties("123") as List<PenaltyAmount>; 

            //  List<TestCWObject> pen = _sm.GetPenalties("123") as List<TestCWObject>;


            //      List<RepaymentAmount> ra = new List<RepaymentAmount>();

            //foreach (var amount in amounts)
            //{
            //    ra.Add(amount as RepaymentAmount);
            //}

            //  List<RepaymentAmount> ra = amounts as List<RepaymentAmount>;



            await Console.Out.WriteLineAsync("Press any key...");
            Console.ReadLine();

            return;

            // var _service = DefaultClientFactory.CreateClient<ICalculator<ICWObject>>(channel);


            //Native grpc call
            /*
            var greetService = new Greeter.GreeterClient(channel);
            var resp =  greetService.SayHello(new HelloRequest2 {Name = "Test" });
            */

            // var res = _service.Sum(new TestCWObject { ID = 1 }, new TestCWObject { ID = 2 });

            //var res2 = _service.SearchGeneric(new TestCWObject { ID = -100 });

            //var to = new BaseTest() { MyProperty = 1 };
            //// var res2 = _service.SearchGeneric(to);



            /*
            (string first, string second, int value, string third) cities = ("Kyiv","Kharkiv",32,"Sumy");
            var city = GetCity(cities);
            */


            //var res = serviceWrapper.CalculateInterestAmount(new TestCWObject { ID = 101 }) ;
            //var obj = serviceWrapper.LoadObject(typeof(TestCWObject), "123");

            string id = "123";
            var obj = serviceWrapper.LoadObject<TestCWObject>(id);
            var lp = new List<IListParams>();
            lp.Add(new ListParams { Id = 1, Name = "One" });
            lp.Add(new ListParams { Id = 2, Name = "Two" });
            lp.Add(new ListParams { Id = 3, Name = "Three" });

            var single = new ListParams { Id = 4, Name = "Four" };


            //IList ls = serviceWrapper.GetParams(lp, single);

            //foreach ( var l in ls ) 
            //{
            //}

            /*
            string contractNumber = "1";
            decimal repaymentAmount = 10;
            decimal penaltyAmount = 11;
            string paymentCurrency = "USD";
            DateTime? date = new DateTime(2023, 4, 25);

            (IList list, decimal repaymentAmount) res = serviceWrapper.GetLoanOneByOnePaymentSplitTest(contractNumber, repaymentAmount, penaltyAmount, paymentCurrency, date);
            repaymentAmount = res.repaymentAmount;
            */

            //serviceWrapper.Export(new TestCWObject { ID = 1000, Title = "Super" }, new List<object> { new object() });

            //serviceWrapper.Export(new TestCWObject { ID = 1000, Title = "Super" },"Test");

            long clientId = 120;

            var r = serviceWrapper.GetClientContracts(clientId);

            await Console.Out.WriteLineAsync("Press any key...");
            Console.ReadLine();

            

            if (Debugger.IsAttached)
            {
                Console.WriteLine("...");
                Console.ReadLine();
            }
        }
    }
}
