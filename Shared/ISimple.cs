using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface ISimple
    {
        [OperationContract]
        SimpleClass GetSimpleClass(int Id, string Name, decimal Price, List<BaseObject> Objects);

        [OperationContract]
        object GetObject(Type type, object o);
    }
}
