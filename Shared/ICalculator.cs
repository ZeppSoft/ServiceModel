using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface ICalculator<TValue>  where TValue : class, ICWObject
    {
        [OperationContract]
        TValue Sum(TValue x, TValue y);
        //[OperationContract]
        //System.Collections.Generic.List<OT> SearchGeneric<OT>(OT sample) where OT : ICWObject;

        [OperationContract]
        // System.Collections.Generic.List<TValue> SearchGeneric<TValue>(TValue sample) where TValue : ICWObject;
        System.Collections.Generic.List<TValue> SearchGeneric(TValue sample);


    }
}
