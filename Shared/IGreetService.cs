using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shared
{
    [ServiceContract(Name = "greet")]
    public interface IGreetService
    {
        [OperationContract(Name = "Sum")]
        Task<HelloReply2> SayHello(HelloRequest2 request);
    }
}
