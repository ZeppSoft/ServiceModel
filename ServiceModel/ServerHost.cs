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
using ServiceModel.Grpc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using System.Runtime.ConstrainedExecution;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Collections;
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

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(new ServerFilter1());
            services.AddSingleton(new ServerFilter2());

            var serviceProvider = services.BuildServiceProvider();

            IReadOnlyList<IMessagePackFormatter> formatters =
              new List<IMessagePackFormatter> { MessagePack.Formatters.TypelessFormatter.Instance, new CWObjectFormatter(), new IListFormatter(), new TestPersonFormatter() };



            IReadOnlyList<IFormatterResolver> resolvers =
                new List<IFormatterResolver> { ContractlessStandardResolver.Instance, StandardResolver.Instance, TypelessContractlessStandardResolver.Instance };




            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
                            formatters,
                        resolvers);


            var opt = MessagePackSerializerOptions.Standard.WithResolver(resolver);


            //_server.Services.AddServiceModelSingleton(
            //    new LoanService(),
            //    options =>
            //    {
            //        // set ProtobufMarshaller as default Marshaller
            //        options.MarshallerFactory = JsonMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
            //    });

            //_server.Services.AddServiceModelSingleton(
            //    new SimpleService(),
            //    options =>
            //    {
            //        // set ProtobufMarshaller as default Marshaller
            //        options.MarshallerFactory = new MessagePackMarshallerFactory(opt);//MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
            //    });

            _server.Services.AddServiceModelSingleton(
              new SomeManager(),
              options =>
              {
                  //options.
                  // set ProtobufMarshaller as default Marshaller
                  options.MarshallerFactory = new MessagePackMarshallerFactory(opt);
                  options.ServiceProvider = serviceProvider;
                 // options.Filters.Add(1, serviceProvider.GetRequiredService<ServerFilter1>());
                  options.Filters.Add(2, serviceProvider.GetRequiredService<ServerFilter2>());

                  //options.MarshallerFactory = MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
              });

            //_server.Services.AddServiceModelSingleton(
            //    new CustomWareService(new SomeManager()),
            //    options =>
            //    {
            //        //options.
            //        // set ProtobufMarshaller as default Marshaller
            //        options.MarshallerFactory = new MessagePackMarshallerFactory(opt);
            //        //options.MarshallerFactory = MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
            //    });
            //_server.Services.AddServiceModelSingleton(
            //    new CalculatorNullableInterfaceService(),
            //    options =>
            //    {
            //        //options.
            //        // set ProtobufMarshaller as default Marshaller
            //        options.MarshallerFactory = new MessagePackMarshallerFactory(opt);
            //        //options.MarshallerFactory = MessagePackMarshallerFactory.Default;//ProtobufMarshallerFactory.Default;
            //    });

           

            //_server.Services.Add(Greeter.BindService(new GreeterService()));

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _server.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => _server.ShutdownAsync();
    }

    internal class ServerFilter1 : IServerFilter
    {
        public ValueTask InvokeAsync(IServerFilterContext context, Func<ValueTask> next)
        {
            return next();
        }
    }


    internal class ServerFilter2 : IServerFilter
    {
        public ValueTask InvokeAsync(IServerFilterContext context, Func<ValueTask> next)
        {

            ValueTask res; 
            try
            {
                OnRequest(context);

                // invoke all other filters in the stack and do service call
               res =  next();
            }
            catch (Exception ex)
            {
                OnError(context, ex);
                throw;
            }

            OnResponse(ref context);

            return res;
        }


        public void OnRequest(IServerFilterContext context)
        {
            // log input
            //LogBegin(logger, context.Request);

            if (context.Request.Count > 0)
            {

                var ar = context.Request.ToArray();

                //foreach (var item in context.Request)
                for (int i = 0; i < ar.Length; i++)
                {
                    try
                    {
                        var ser = ar[i].Value as TestPersonSerialized;

                        if (ser != null)
                        {
                            var person = PersonSerializeHelper.DoDeConvert(ser);
                            ar[i] = new KeyValuePair<string, object>(ar[i].Key, person);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }


        }

        public void OnResponse(ref IServerFilterContext context)
        {


           var sr = context.ServiceInstance;
           

            if (context.Response.IsProvided)
            {
                if (context.Response.Count > 0)
                {
                    var f = context.Response.First();

                    f    = new KeyValuePair<string, object>("",new object());


                   // context.Response["result"] = null; 
                    context.UserState.Add("", new object());

                    context = new ServerContextWrapper(context);
                    // KeyValuePair<string, object> st;
                    //  context.Response["result"] = new TestPersonSerialized();

                    //  var t = (IResponseContext)context.Response.First();


                    //foreach (var item in context.Response)
                    //{
                    //    st = item;
                    //}

                    // st =  KeyValuePair<string, object>("",new object());
                    //for (int i = 0; i < context.Response.Count; i++)
                    //{
                    //    //var item = context.Response[i];

                    //    context.Response.;

                    //}


                    //context.Response[0] = new KeyValuePair<string, object>("", new object());


                    //context.Response[0] = new object();// new KeyValuePair<string, object>(/*ob.Key*/"", new object());

                    //foreach (var item in context.Response)
                    //{
                    //    item = new KeyValuePair<string, object>(ar[i].Key, ser);
                    //}



                    //for (int i = 0; i < context.Response.Count; i++)
                    //{
                    //   // context.Response[i] = new KeyValuePair<string, object>("", new object());

                    //    try
                    //    {

                    //        var ob = (KeyValuePair<string, object>)context.Response[i];

                    //        var person = ob.Value as TestPerson;

                    //        if (person != null)
                    //        {
                    //            var ser = PersonSerializeHelper.DoConvert(person);
                    //            context.Response[i] = new KeyValuePair<string, object>(/*ob.Key*/"", new object());
                    //        }
                    //    }
                    //    catch (Exception)
                    //    {

                    //        throw;
                    //    }
                    //}

                }
            }
            else
            {
                // warn: the service method was not called
                //logger.LogWarning(message.ToString());
            }

            // log server stream in case of Server/Duplex streaming


            // log output
            //LogEnd(logger, context.Response);
        }

        public void OnError(IServerFilterContext context, Exception error)
        {
            // log exception
            // logger.LogError("error {0} failed: {1}", context.ContractMethodInfo.Name, error);
        }
    }

    public class ServerContextWrapper : IServerFilterContext
    {
        IServerFilterContext _contex;
        ServiceResponseContextWrapper _wrapper;
        public ServerContextWrapper(IServerFilterContext context)
        {
            _contex = context;
            _wrapper = new ServiceResponseContextWrapper(_contex.Response);
        }
        public object ServiceInstance
        {
            get { return _contex.ServiceInstance; }
        }

        public ServerCallContext ServerCallContext => throw new NotImplementedException();

        public IServiceProvider ServiceProvider => throw new NotImplementedException();

        public IDictionary<object, object> UserState => throw new NotImplementedException();

        public MethodInfo ContractMethodInfo => throw new NotImplementedException();

        public MethodInfo ServiceMethodInfo => throw new NotImplementedException();

        public IRequestContext Request => throw new NotImplementedException();

        public IResponseContext Response
        {
            get => _wrapper;
        }
    }

    public class ServiceResponseContextWrapper : IResponseContext
    {
        IResponseContext _context;
        object[] args;
        public ServiceResponseContextWrapper(IResponseContext context)
        {
            _context = context;
            args = new object[_context.Count];
            args[0] = new TestPersonSerialized();

        }
        public object this[string name] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object this[int index] { get => args[index]; set { args[index] = value; } }

        public bool IsProvided => throw new NotImplementedException();

        public int Count => _context.Count;

        public object Stream { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
