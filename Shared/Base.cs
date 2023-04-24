using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared
{
    public class BaseObject
    {
        public string Value { get; set; }
    }

    public class SimpleClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        //[JsonIgnore]
        public List<BaseObject> Objects { get; set; }


    }
}
