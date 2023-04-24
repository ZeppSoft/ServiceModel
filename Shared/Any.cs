using ProtoBuf.Meta;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace Shared
{
    [DataContract]
    public class Container
    {
        /// <summary></summary>
        [DataMember(Order = 1, Name = nameof(Value))]
        public Any.Container Any { get => new Any.Container(Value); set => Value = value.Value; }
        /// <summary>Object</summary>
        public object Value;

        [DataMember(Order = 2, Name = nameof(Type))]
        public Type Type { get; set; }
    }

    [ProtoContract(Name = "type.googleapis.com/google.protobuf.Any")]
    public class Any
    {

        /// <summary>Pack <paramref name="value"/></summary>
        public static Any Pack(object value)
        {
            // Handle null
            if (value == null) return new Any { TypeUrl = null, Value = Array.Empty<byte>() };
            // Get type
            System.Type type = value.GetType();
            // Write here
            MemoryStream ms = new MemoryStream();
            // Serialize
            RuntimeTypeModel.Default.Serialize(ms, value);
            // Create any
            Any any = new Any
            {
                TypeUrl = $"{type.Assembly.GetName().Name}/{type.FullName}",
                Value = ms.ToArray()
            };
            // Return
            return any;
        }

        /// <summary>Unpack any record</summary>
        public object Unpack()
        {
            // Handle null
            if (TypeUrl == null || Value == null || Value.Length == 0) return null;
            // Find '/'
            int slashIx = TypeUrl.IndexOf('/');
            // Convert to C# type name
            string typename = slashIx >= 0 ? $"{TypeUrl.Substring(slashIx + 1)}, {TypeUrl.Substring(0, slashIx)}" : TypeUrl;
            // Get type (Note security issue here!)
            System.Type type = System.Type.GetType(typename, true);
            // Deserialize
            object value = RuntimeTypeModel.Default.Deserialize(type, Value.AsMemory());

            //MemoryStream ms = new MemoryStream(Value);
            //object value = RuntimeTypeModel.Default.Deserialize(ms, Value, type);


            // Return
            return value;
        }

        /// <summary>Test type</summary>
        public bool Is(System.Type type) => $"{type.Assembly.GetName().Name}/{type.FullName}" == TypeUrl;

        /// <summary>Type url (using C# type names)</summary>
        [ProtoMember(1)]
        public string TypeUrl = null;
        /// <summary>Data serialization</summary>
        [ProtoMember(2)]
        public byte[] Value = null;

        /// <summary></summary>
        public static implicit operator Container(Any value) => new Container(value.Unpack());
        /// <summary></summary>
        public static implicit operator Any(Container value) => Any.Pack(value.Value);

        /// <summary></summary>
        public class Container
        {
            /// <summary></summary>
            public object Value;
            /// <summary></summary>
            public Container()
            {
                this.Value = null;
            }

            /// <summary></summary>
            public Container(object value)
            {
                this.Value = value;
            }
        }
    }
}
