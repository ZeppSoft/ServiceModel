using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ProtoContract]
    public class LoanApplication
    {
        [ProtoMember(1)]
        public string LoanApplicationID { get; set; }
        [ProtoMember(2)]
        public List<Payment> Payments { get; set; }
    }

    [ProtoContract]
    public class Payment
    {
        [ProtoMember(1)]
        public DateTime PaymentDate { get; set; }

        [ProtoMember(2)]
        public decimal Amount { get; set; }
    }

    //public class LoadObjectResponse
    //{
    //    public T LoadObject<T>(string id, bool fromDatabase) where T : class
    //    {
    //        return new LoanApplication() as T;
    //    }
    //}
}
