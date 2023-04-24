using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProtoBuf.Meta;
using ServiceModel.Grpc.Configuration;

namespace Shared
{
    public sealed class JsonMarshallerFactory : IMarshallerFactory
    {
        public static readonly IMarshallerFactory Default = new JsonMarshallerFactory();

        public JsonMarshallerFactory()
            : this(CreateDefaultOptions())
        {
        }

        public JsonMarshallerFactory(JsonSerializerOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            Options = options;
        }

        public JsonSerializerOptions Options { get; }

        public Marshaller<T> CreateMarshaller<T>()
        {
            return new Marshaller<T>(Serialize<T>, Deserialize<T>);
        }

        private static JsonSerializerOptions CreateDefaultOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        private void Serialize<T>(T value, SerializationContext context)
        {
            var bufferWriter = context.GetBufferWriter();
            var jsonWriter = new Utf8JsonWriter(bufferWriter);

           
            //MemoryStream ms = new MemoryStream();
            //// Serialize
            //RuntimeTypeModel.Default.Serialize(ms, value);


            JsonSerializer.Serialize(jsonWriter, value, Options);
            context.Complete();
        }

        private T Deserialize<T>(DeserializationContext context)
        {
            var jsonData = context.PayloadAsReadOnlySequence();
            var reader = new Utf8JsonReader(jsonData);
            return JsonSerializer.Deserialize<T>(ref reader, Options);


            //MemoryStream ms = new MemoryStream(Value);
            //object value = RuntimeTypeModel.Default.Deserialize(ms, Value, type);
            //return value;
        }
    }
}