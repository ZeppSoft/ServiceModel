using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using ProtoBuf.Serializers;
using ServiceModel.Grpc.Configuration;
using ServiceModel.Grpc.Filters;
using Shared.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
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

            IReadOnlyList<IMessagePackFormatter> formatters = 
                new List<IMessagePackFormatter> { MessagePack.Formatters.TypelessFormatter.Instance, new CWObjectFormatter() };



            IReadOnlyList<IFormatterResolver> resolvers = 
                new List<IFormatterResolver> { ContractlessStandardResolver.Instance,StandardResolver.Instance,TypelessContractlessStandardResolver.Instance };


           

            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
                            formatters,
                        resolvers);


            var options = MessagePackSerializerOptions.Standard.WithResolver(resolver);

            var result1 = MessagePackSerializer.Serialize(
                                                               value,
                                                               options
                                                               //MessagePack.Resolvers.TypelessContractlessStandardResolver.Options 
                                                               );

            context.Complete(result1);
        }

        private T Deserialize<T>(DeserializationContext context)
        {
            var data = context.PayloadAsNewBuffer();//context.PayloadAsReadOnlySequence();

            //            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
            //new[] { MessagePack.Formatters.TypelessFormatter.Instance },
            //new[] { ContractlessStandardResolver.Instance });

            IReadOnlyList<IMessagePackFormatter> formatters =
                new List<IMessagePackFormatter> { MessagePack.Formatters.TypelessFormatter.Instance, new CWObjectFormatter()};



            IReadOnlyList<IFormatterResolver> resolvers =
                new List<IFormatterResolver> { ContractlessStandardResolver.Instance, StandardResolver.Instance, TypelessContractlessStandardResolver.Instance };




            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
                            formatters,
                        resolvers);

            var options = MessagePackSerializerOptions.Standard.WithResolver(resolver);
            var des = MessagePackSerializer.Deserialize<T>(data
                            ,options 
                            //,MessagePack.Resolvers.TypelessContractlessStandardResolver.Options
                            );

            return des;
        }
    }

    class RepaymentAmountFormatter : IMessagePackFormatter<RepaymentAmount>
    {
        public RepaymentAmount Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var Amount = options.Resolver.GetFormatterWithVerify<decimal>().Deserialize(ref reader, options);
            var Tax = options.Resolver.GetFormatterWithVerify<decimal>().Deserialize(ref reader, options);
            var Type = options.Resolver.GetFormatterWithVerify<Type>().Deserialize(ref reader, options);

            var dto = (RepaymentAmount)Activator.CreateInstance(Type);
            dto.Amount = Amount;
            dto.Tax = Tax;

            return dto;
        }

        public void Serialize(ref MessagePackWriter writer, RepaymentAmount value, MessagePackSerializerOptions options)
        {
            options.Resolver.GetFormatterWithVerify<decimal>().Serialize(ref writer, value.Amount, options);
            options.Resolver.GetFormatterWithVerify<decimal>().Serialize(ref writer, value.Tax, options);
            options.Resolver.GetFormatterWithVerify<Type>().Serialize(ref writer, value.GetType(), options);
        }
    }
    public class IListFormatter : IMessagePackFormatter<IList>
    {
        // public  class MessagePackReaderHolder
        //{
        //    public MessagePackReaderHolder(ref MessagePackReader r)
        //    {
        //        Reader = r;
        //    }
        //   public ref MessagePackReader Reader ;
        //}

        public writerDelegate2<T> SetGeneric<T>(MethodCallExpression call, ParameterExpression p1, ParameterExpression p2,
                                ParameterExpression p3, MethodInfo method, ConstantExpression instance) 
        {
            var expression2 = Expression.Lambda<writerDelegate2<T>>(call, p1, p2,p3);

            var func = expression2.Compile();

            

            return func;

            // return expression2;
        }
        //public static MessagePackReader DoSomething(in MessagePackReader bob)
        //=> bob;
        //delegate MessagePackReader myDelegate(in MessagePackReader asd);

        //delegate T myDelegate2<T>(in MessagePackReader asd,MessagePackSerializerOptions options) where T: class;
        delegate object readerDelegate(in MessagePackReader rdr, MessagePackSerializerOptions options);
        delegate void writerDelegate(in MessagePackWriter wrt, RepaymentAmount value, MessagePackSerializerOptions options);
     public   delegate void writerDelegate2<T>(in MessagePackWriter wrt, T value, MessagePackSerializerOptions options);
        delegate void writerDelegate3(in MessagePackWriter wrt, object value, MessagePackSerializerOptions options);


        public IList Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
           // List<RepaymentAmount> l = new List<RepaymentAmount>();

            var type = options.Resolver.GetFormatterWithVerify<Type>().Deserialize(ref reader, options);

            //Type genericListType = typeof(List<>).MakeGenericType(type);
            //var l = (IList)Activator.CreateInstance(genericListType);

             var count = options.Resolver.GetFormatterWithVerify<int>().Deserialize(ref reader, options);

            var elemType = options.Resolver.GetFormatterWithVerify<Type>().Deserialize(ref reader, options);

            Type genericListType = typeof(List<>).MakeGenericType(elemType);
            var l = (IList)Activator.CreateInstance(genericListType);


            for (int i = 0; i < count; i++)
            {
                //var r = options.Resolver.GetFormatterWithVerify<object>().Deserialize(ref reader, options);
                l.Add(options.Resolver.GetFormatterWithVerify<object>().Deserialize(ref reader, options));

            }

            return l;












            var t = options.Resolver.GetFormatterDynamic(elemType) as MessagePack.Formatters.IMessagePackFormatter;

          
            /*
            var method = typeof(IListFormatter).GetMethod("DoSomething", new[] { typeof(MessagePackReader).MakeByRefType() });
            var parameter = Expression.Parameter(typeof(MessagePackReader).MakeByRefType(), "b");
            var call = Expression.Call(method, parameter);
            var expression = Expression.Lambda<myDelegate>(call, parameter);
            var func = expression.Compile();
            var result = func(reader);
            */




          



            var method2 = t.GetType().GetMethod(nameof(Deserialize), new[] { typeof(MessagePackReader).MakeByRefType(), typeof(MessagePackSerializerOptions) });
            var parameter1 = Expression.Parameter(typeof(MessagePackReader).MakeByRefType(), "reader");
            var parameter2 = Expression.Parameter(typeof(MessagePackSerializerOptions), "options");
            var instance = Expression.Constant(t);

            var call2 = Expression.Call(instance, method2, parameter1,parameter2);
            //var expression2 = Expression.Lambda<myDelegate2<RepaymentAmount>>(call2, parameter1,parameter2);
            var expression2 = Expression.Lambda<readerDelegate>(call2, parameter1, parameter2);
            var func2 = expression2.Compile(); 
            var result2 = func2(reader,options);
            var result3 = func2(reader, options);


            //var result4 =  Convert.ChangeType(result2, type);

          


            l.Add(result2);


            for (int i = 0; i < count; i++)
            {
                l.Add(options.Resolver.GetFormatterWithVerify<RepaymentAmount>().Deserialize(ref reader, options));

            }


            //var id = options.Resolver.GetFormatterWithVerify<RepaymentAmount>().Deserialize(ref reader, options);


            return l;
            //throw new NotImplementedException();
        }

        public void Serialize(ref MessagePackWriter writer, IList value, MessagePackSerializerOptions options)
        {
            options.Resolver.GetFormatterWithVerify<Type>().Serialize(ref writer, value.GetType(), options);
            options.Resolver.GetFormatterWithVerify<int>().Serialize(ref writer, value.Count, options);
            options.Resolver.GetFormatterWithVerify<Type>().Serialize(ref writer, value[0].GetType(), options);
            var t = options.Resolver.GetFormatterDynamic(value[0].GetType()) as MessagePack.Formatters.IMessagePackFormatter;

            foreach (var item in value)
            {
                options.Resolver.GetFormatterWithVerify<object>().Serialize(ref writer, item, options);
            }



            //foreach (var item in value)
            //{

                //    var method = t.GetType().GetMethod(nameof(Serialize), new[] { typeof(MessagePackWriter).MakeByRefType(), item.GetType(), typeof(MessagePackSerializerOptions) });
                //    var parameter1 = Expression.Parameter(typeof(MessagePackWriter).MakeByRefType(), "writer");
                //    var parameter2 = Expression.Parameter(item.GetType(), "value");
                //    var parameter3 = Expression.Parameter(typeof(MessagePackSerializerOptions), "options");

                //    var instance = Expression.Constant(t);

                //    var call = Expression.Call(instance, method, parameter1, parameter2,parameter3);




                //    //Using reflection
                //    var baseMethod = typeof(IListFormatter).GetMethod(nameof(SetGeneric));
                //    var genericMethod = baseMethod.MakeGenericMethod(item.GetType());

                //    object[] arr = new object[6];
                //    arr[0] = call;
                //    arr[1] = parameter1;
                //    arr[2] = parameter2;
                //    arr[3] = parameter3;
                //    arr[4] = method;
                //    arr[5] = instance;



                //    dynamic func2 = genericMethod.Invoke(this, arr);

                //    //func2(writer, item, options);
                //    func2(writer, item, options);



                //    // var expression = Expression.Lambda<writerDelegate>(call, parameter1, parameter2, parameter3);
                //    var expression = Expression.Lambda<writerDelegate>(call, parameter1, parameter2, parameter3);

                //    var func = expression.Compile();

                //     func(writer,(RepaymentAmount)item, options);
                //    //func(writer, item, options);


                //    // options.Resolver.GetFormatterWithVerify<RepaymentAmount>().Serialize(ref writer, item as RepaymentAmount, options);
                //}

                //throw new NotImplementedException();
        }
    }





    public class TestPersonFormatter : IMessagePackFormatter<TestPerson>
    {
        public void Serialize(ref MessagePackWriter writer, TestPerson value, MessagePackSerializerOptions options)
        {

            options.Resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.FirstName, options);
            options.Resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.LastName, options);
        }

        public TestPerson Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var firstName = options.Resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            var lastName = options.Resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
           
            var person = new TestPerson() { FirstName  = firstName,LastName = lastName};
            return person;
        }
    }


    public class TestPersonSerializedFormatter : IMessagePackFormatter<TestPersonSerialized>
    {
        public void Serialize(ref MessagePackWriter writer, TestPersonSerialized value, MessagePackSerializerOptions options)
        {
            options.Resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Name, options);
        }

        public TestPersonSerialized Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var name = options.Resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);

            var person = new TestPersonSerialized() { Name = name };
            return person;
        }
    }


    public static class PersonSerializeHelper
    {
        public static TestPersonSerialized DoConvert(TestPerson person)
        {
            var ser = new TestPersonSerialized();
            ser.Name = person.FirstName + "_" + person.LastName;
            return ser;
        }

        public static TestPerson DoDeConvert(TestPersonSerialized ser)
        {
            string[] words = ser.Name.Split('_');
            var person = new TestPerson();
            person.FirstName = words[0];
            person.LastName = words[1];
            return person;
        }
    }





    // serialize/deserialize internal field.
    public class CWObjectFormatter : IMessagePackFormatter<ICWObject>
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


    //class SomeManagerFormatter : IMessagePackFormatter<ISomeManager>
    //{
    //    public void Serialize(ref MessagePackWriter writer, ISomeManager value, MessagePackSerializerOptions options)
    //    {

    //        //var blob = MessagePackSerializer.Serialize(value, TypelessContractlessStandardResolver.Options);

    //        DtoSomeManager value2 = value as DtoSomeManager;

    //        options.Resolver.GetFormatterWithVerify<DtoSomeManager>().Serialize(ref writer, value2, TypelessContractlessStandardResolver.Options);

    //        //options.Resolver.GetFormatterWithVerify<Type>().Serialize(ref writer, value.GetType(), options);

    //        //DtoSomeManager mc = new DtoSomeManager();
    //        // var blob = MessagePackSerializer.Typeless.Serialize(mc);
    //        //options.Resolver.GetFormatterWithVerify < byte[]>().Serialize(ref writer, blob, options);
    //        //options.Resolver.GetFormatterWithVerify<DtoSomeManager>().Serialize(ref writer, mc, options);
    //    }

    //    public ISomeManager Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    //    {
    //        //var type = options.Resolver.GetFormatterWithVerify <DtoSomeManager> ().Deserialize(ref reader, options);

    //        var o = options.Resolver.GetFormatterWithVerify<DtoSomeManager>().Deserialize(ref reader, TypelessContractlessStandardResolver.Options);

    //        return o as ISomeManager;
    //        //var objModel = MessagePackSerializer.Typeless.Deserialize(bin) as MyClass;

    //        //var type = options.Resolver.GetFormatterWithVerify<Type>().Deserialize(ref reader, options);

    //        //var dto = Activator.CreateInstance(type) as ISomeManager;

    //        //return dto as ISomeManager;
    //    }
    //}

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





    


    //internal class ServerFilter1 : IServerFilter
    //{
    //    public ValueTask InvokeAsync(IServerFilterContext context, Func<ValueTask> next)
    //    {
    //        return next();
    //    }
    //}


    //internal class ServerFilter2 : IServerFilter
    //{
    //    public ValueTask InvokeAsync(IServerFilterContext context, Func<ValueTask> next)
    //    {
    //        return next();
    //    }
    //}

}
