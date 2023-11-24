using GrpcService3.Services;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Shared.Interfaces;
using System.IO.Compression;
using MessagePackMarshallerFactory = ServiceModel.Grpc.Configuration.MessagePackMarshallerFactory;

namespace GrpcService3
{
    public class Program
    {
        //private const int Port = 5195;
        //private static async Task<IHost> StartWebHost()
        //{
        //    var host = Host
        //        .CreateDefaultBuilder()
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //            webBuilder.UseKestrel(o => o.ListenLocalhost(Port, l => l.Protocols = HttpProtocols.Http2));

        //        })
        //        .Build();

        //    await host.StartAsync();
        //    return host;
        //}
        public static /*async Task*/ void Main(string[] args)
        {

            //using (var host = await StartWebHost())
            //{
            //    await host.StopAsync();
            //}


        



            
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            builder.Services.AddGrpc(options =>
                                     options.ResponseCompressionLevel = CompressionLevel.Optimal);



            builder.Services.AddSingleton<ISomeManager>(new SomeManager());




            IReadOnlyList<IMessagePackFormatter> formatters =
              new List<IMessagePackFormatter> { MessagePack.Formatters.TypelessFormatter.Instance, new TestPersonFormatter() };



            IReadOnlyList<IFormatterResolver> resolvers =
                new List<IFormatterResolver> { ContractlessStandardResolver.Instance, StandardResolver.Instance, TypelessContractlessStandardResolver.Instance };




            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
                            formatters,
                        resolvers);


            var opt = MessagePackSerializerOptions.Standard.WithResolver(resolver);











            // enable ServiceModel.Grpc
            builder.Services
                 .AddServiceModelGrpcServiceOptions<SomeManager>(options =>
                 {
                     options.MarshallerFactory = new MessagePackMarshallerFactory(opt);//MessagePackMarshallerFactory.Default;
                 });

            builder.Services.AddServiceModelGrpc();
            //builder.Services.AddServiceModelGrpc(options =>
            // {
            //     // set MessagePackMarshaller as default Marshaller
            //     options.DefaultMarshallerFactory = MessagePackMarshallerFactory.Default;
            // });



            var app = builder.Build();


            app.UseRouting();
            app.UseGrpcWeb();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<SomeManager>().EnableGrpcWeb();
            });

            //app.UseGrpcWeb
            // app.Services.GetServices

            // Configure the HTTP request pipeline.
           // app.MapGrpcService<SomeManager>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

           

            app.Run();

           
        }
    }
}