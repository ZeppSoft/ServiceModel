using Google.Protobuf.WellKnownTypes;
using ServiceModel.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface ILoanService
    {
        [OperationContract]
        int Add(int x, int y);
        [OperationContract]
        decimal GetNextPaymentAmount(string loanApplicationId, DateTime date);

        //[OperationContract]
        //// T LoadObject<T>(object id, bool fromDatabase) where T : class;
        //LoadObjectResponse LoadObject(string id, bool fromDatabase);
        [OperationContract]
        object LoadObject(System.Type type, Object id);

        [OperationContract]
        T LoadObject2<T>(object id) where T : class;

        [OperationContract]
        Task<HelloReply> TestContainer(Container request,
          CallContext context = default);
    }
   

    [DataContract]
    public class HelloReply
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}
