using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using ProtoBuf.Serializers;
using ServiceModel.Grpc.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Type = System.Type;

namespace Shared
{
    public sealed class MessagePackMarshallerFactory : IMarshallerFactory
    {
        public static readonly IMarshallerFactory Default = new MessagePackMarshallerFactory();
        public Marshaller<T> CreateMarshaller<T>()
        {
            return new Marshaller<T>(Serialize<T>, Deserialize<T>);
        }
        //    private void Serialize<T>(T value, SerializationContext context)
        //    {

        //        // var result1 = MessagePackSerializer.Typeless.Serialize(value);
        //        // context.Complete(result1);

        ////        var resolver = MessagePack.Resolvers.CompositeResolver.Create(
        ////new[] { MessagePack.Formatters.TypelessFormatter.Instance },
        ////new[] { ContractlessStandardResolver.Instance });


        //        var bin = MessagePackSerializer.Serialize(
        //                                                    value,
        //                                                    MessagePack.Resolvers.ContractlessStandardResolver.Options);




        //        //var options = MessagePackSerializerOptions.Standard.WithResolver(resolver);
        //        //MessagePackSerializer.DefaultOptions = options;
        //        //var bin = MessagePackSerializer.Serialize(value, options);


        //        // {"MyProperty1":99,"MyProperty2":9999}
        //        Console.WriteLine(MessagePackSerializer.ConvertToJson(bin));

        //                    // You can also set ContractlessStandardResolver as the default.
        //                    // (Global state; Not recommended when writing library code)
        //                    MessagePackSerializer.DefaultOptions = MessagePack.Resolvers.ContractlessStandardResolver.Options;

        //                    // Now serializable...
        //                    var bin2 = MessagePackSerializer.Serialize(value);

        //                    context.Complete(bin2);

        //    }
        private void Serialize<T>(T value, SerializationContext context)
        {
            //var result1 = MessagePackSerializer.Typeless.Serialize(value);
            var result1 = MessagePackSerializer.Serialize(
                                                               value,
                                                               MessagePack.Resolvers.ContractlessStandardResolver.Options);

            context.Complete(result1);

            /*

            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
    new[] { MessagePack.Formatters.TypelessFormatter.Instance },
    new[] { ContractlessStandardResolver.Instance });






            var options = MessagePackSerializerOptions.Standard.WithResolver(resolver);
            MessagePackSerializer.DefaultOptions = options;
            var bin = MessagePackSerializer.Serialize(value, options);


            // {"MyProperty1":99,"MyProperty2":9999}
            Console.WriteLine(MessagePackSerializer.ConvertToJson(bin));

            // You can also set ContractlessStandardResolver as the default.
            // (Global state; Not recommended when writing library code)
            MessagePackSerializer.DefaultOptions = MessagePack.Resolvers.ContractlessStandardResolver.Options;

            // Now serializable...
            var bin2 = MessagePackSerializer.Serialize(value);

            context.Complete(bin2);
            */

        }

        private T Deserialize<T>(DeserializationContext context)
        {
            var data = context.PayloadAsNewBuffer();//context.PayloadAsReadOnlySequence();

            //MessagePackSerializer.DefaultOptions = MessagePack.Resolvers.TypelessContractlessStandardResolver.Options;
            //var ses = MessagePackSerializer.Typeless.Deserialize(data);

            MessagePackSerializer.DefaultOptions = MessagePack.Resolvers.ContractlessStandardResolver.Options;
            var des = MessagePackSerializer.Deserialize<T>(data);

            return des;
        }
    }

    // serialize/deserialize internal field.
    class CWObjectFormatter : IMessagePackFormatter<ICWObject>
    {
        public void Serialize(ref MessagePackWriter writer, ICWObject value, MessagePackSerializerOptions options)
        {

            options.Resolver.GetFormatterWithVerify<int>().Serialize(ref writer, (int)value.ID, options);
            options.Resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Title, options);
            options.Resolver.GetFormatterWithVerify<bool>().Serialize(ref writer, value.IsDirty, options);
            options.Resolver.GetFormatterWithVerify<bool>().Serialize(ref writer, value.WasRemoved, options);
            options.Resolver.GetFormatterWithVerify<Type>().Serialize(ref writer, value.GetType(), options);





            //options.Resolver.GetFormatter<DtoCWObject>().Serialize(ref writer, value as ICWObject, options);

            //options.Resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.ID.ToString(), options);



            //options.Resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.internalId, options);
        }

        public ICWObject Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {

            var id = options.Resolver.GetFormatterWithVerify<int>().Deserialize(ref reader, options);
            var title = options.Resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            var isDirty = options.Resolver.GetFormatterWithVerify<bool>().Deserialize(ref reader, options);
            var wasRemoved = options.Resolver.GetFormatterWithVerify<bool>().Deserialize(ref reader, options);
            var type = options.Resolver.GetFormatterWithVerify<Type>().Deserialize(ref reader, options);


            var dto = Activator.CreateInstance(type) as ICWObject;

            dto.ID = id;
            dto.Title = title;

            return dto as ICWObject;

            // var id = options.Resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
        }
    }


    class ListParamFormatter : IMessagePackFormatter<IListParams>
    {
        public void Serialize(ref MessagePackWriter writer, IListParams value, MessagePackSerializerOptions options)
        {
            options.Resolver.GetFormatterWithVerify<int>().Serialize(ref writer, (int)value.Id, options);
            options.Resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Name, options);
            options.Resolver.GetFormatterWithVerify<Type>().Serialize(ref writer, value.GetType(), options);
        }

        public IListParams Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {

            var id = options.Resolver.GetFormatterWithVerify<int>().Deserialize(ref reader, options);
            var name = options.Resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            var type = options.Resolver.GetFormatterWithVerify<Type>().Deserialize(ref reader, options);

            var dto = Activator.CreateInstance(type) as IListParams;

            dto.Id = id;
            dto.Name = name;

            return dto as IListParams;
        }
    }












}
