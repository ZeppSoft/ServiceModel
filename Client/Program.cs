using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProtoBuf.Meta;
using ServiceModel.Grpc.Client;
using ServiceModel.Grpc.Configuration;
using Shared;
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
using MessagePackMarshallerFactory = Shared.MessagePackMarshallerFactory;

namespace Client
{
    internal class Program
    {
        public static string GetCity((string first, string second, int value, string third) cityName)
        {
           
            return cityName.first;
        }

        public static async Task Main(string[] args)
        {
            Thread.Sleep(3000);
            ICustomWareNET serviceWrapper = new ServiceWrapper();

            /*
            (string first, string second, int value, string third) cities = ("Kyiv","Kharkiv",32,"Sumy");
            var city = GetCity(cities);
            */


            //var res = serviceWrapper.CalculateInterestAmount(new TestCWObject { ID = 101 }) ;
            // var obj = serviceWrapper.LoadObject(typeof(TestCWObject), "123");

            string id = "123";
            //var obj = serviceWrapper.LoadObject<TestCWObject>("111");
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
