using Grpc.Core.Interceptors;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpc.Core.Interceptors.Interceptor;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Client
{
    public class ClientLoggerInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {

            var response = await base.UnaryServerHandler(request, context, continuation);

            return response;
        }

        
    }
}
