using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel
{
    public class SimpleService : ISimple
    {
        public object GetObject(Type type, object o)
        {
            var t = Activator.CreateInstance(type);
            return t;
        }

        public SimpleClass GetSimpleClass(int Id, string Name, decimal Price, List<BaseObject> Objects)
        {
            var simple = new SimpleClass { Id = Id, Name = Name, Price = Price,Objects = Objects };
            return simple;
        }
    }
}
