using GrpcService3.Services;

namespace GrpcService3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceModelGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // 
                endpoints.MapGrpcService<SomeManager>();
            });
        }
    }
}