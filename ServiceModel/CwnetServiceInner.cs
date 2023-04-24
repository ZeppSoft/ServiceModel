using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel
{
    public interface ICustomWareNETInner 
    {
        int GetApplicationServerID();
        TimeSpan GetCurrentTime();

        DateTime GetCurrentDate();
        DateTime GetActualDate();


        decimal MainRateValue(ICWObject value);
        decimal CalculateInterestAmount(ICWObject value);
        decimal CalculateInterestAmount(ICWObject value, decimal mainRateValue);
        /// <summary>
        /// returns service parameter
        /// see <see cref="T:Quipu.CustomWareNET.Interfaces.Objects.IServiceParameter">IServiceParameter</see>
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// exports object (anExport procedure will be called)
        /// </summary>
        /// <param name="value"></param>
        void Export(ICWObject value);
        /// <summary>
        /// Exports the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameters">The parameters.</param>
        void Export(ICWObject value, params object[] parameters);
        /// <summary>
        /// search for objects that are equals to the sample
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        IList Search(ICWObject sample);
        /// <summary>
        /// search for objects that are equals to the sample
        /// </summary>
        /// <typeparam name="OT"></typeparam>
        /// <param name="sample"></param>
        /// <returns></returns>
        System.Collections.Generic.List<OT> SearchGeneric<OT>(OT sample) where OT : ICWObject;
        //      /// <summary>
        //      /// search for objects by search condition
        //      /// </summary>
        //      /// <param name="searchCondition">search condition. Something like Field = Value</param>
        //      /// <param name="objectType">object type or interface</param>
        //      /// <returns>List of found objects</returns>
        //[Obsolete("sqli")]
        //      IList Search(string searchCondition, Type objectType);
        //      /// <summary>
        //      /// search for objects by search condition
        //      /// </summary>
        //      /// <typeparam name="OT"></typeparam>
        //      /// <param name="searchCondition"></param>
        //      /// <returns></returns>
        //[Obsolete("sqli")]
        //      System.Collections.Generic.IList<OT> SearchGenericString<OT>(string searchCondition) where OT : ICWObject;
        /// <summary>
        /// searches one object from list that meets criteria
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        T SearchOne<T>(T sample, bool retSample = false) where T : ICWObject;
        /// <summary>
        /// searches object with interface IHaveCode by it's Code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        T SearchOneByCode<T>(string code) where T : ICWObject;
        /// <summary>
        /// loads objects from database|list
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <returns></returns>
        IList Load(Type type);
        /// <summary>
        /// loads objects from database|list
        /// </summary>
        /// <typeparam name="T">type of objects to load</typeparam>
        /// <returns></returns>
        System.Collections.Generic.List<T> Load<T>() where T : class, ICWObject;
        ///// <summary>
        ///// loads objects from database|list accordingly to filter
        ///// </summary>
        ///// <param name="filter">keys should be procedure parameters names, values sould be parameter values</param>
        ///// <param name="type">type or interface</param>
        ///// <returns></returns>
        //IList Load(IDictionary filter, Type type);
        /// <summary>
        /// loads object from the database
        /// </summary>
        /// <typeparam name="T">type or interface</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T LoadObject<T>(object id) where T : class, ICWObject;
        /// <summary>
        /// loads object from the database
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <param name="id"></param>
        /// <returns></returns>
        object LoadObject(Type type, object id);
        /// <summary>
        /// loads object from database
        /// </summary>
        /// <typeparam name="T">type or interface</typeparam>
        /// <param name="id"></param>
        /// <param name="fromDatabase">if true - object will be reloaded</param>
        /// <returns></returns>
        T LoadObject<T>(object id, bool fromDatabase) where T : class, ICWObject;
        /// <summary>
        /// loads or create object from the database
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <param name="id"></param>
        /// <returns></returns>
        T LoadOrCreate<T>(object id) where T : class, ICWObject, new();
        /// <summary>
        /// saves object to the database
        /// </summary>
        /// <param name="value"></param>
        /// <summary>
        /// Save for Business Process with list of objects, which need BPID property from saved business process
        /// </summary>
        /// <param name="businessProcess">Business process for save</param>
        /// <param name="items">items for save</param>
        /// <returns></returns>
        ICWObject Save(ICWObject value);
        T SaveG<T>(T value) where T : class, ICWObject;
        /// <summary>
        /// saves object list to the database
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IList Save(IList value);
        /// <summary>
        /// deletes objects in the database
        /// </summary>
        /// <param name="value"></param>
        void Delete(IList value);
        /// <summary>
        /// deletes object in the database
        /// </summary>
        /// <param name="value"></param>
        void Delete(ICWObject value);
        /// <summary>
        /// loads object with specified id from database/list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">type or interface</param>
        /// <returns></returns>
        ICWObject Find(object id, Type type);
        /// <summary>
        /// new object specified type
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <returns></returns>
        ICWObject New(Type type);
        /// <summary>
        /// new object of specified type
        /// </summary>
        /// <typeparam name="T">type or interface</typeparam>
        /// <param name="clearFields">reset property values or not</param>
        /// <returns></returns>
        T New<T>(bool clearFields = true);
        /// <summary>
        /// clones object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        T Clone<T>(T source) where T : ICWObject;
        /// <summary>
        /// new objects list 
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <param name="count">how many new objects will be returned</param>
        /// <returns></returns>
        IList New(Type type, int count);
        /// <summary>
        /// new object of the specified type|interface 
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <param name="clearFields">object property values will be set to default values if true</param>
        /// <returns></returns>
        ICWObject New(Type type, bool clearFields);
        /// <summary>
        /// returns application menu for logged user
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Gets the application menu.
        /// </summary>
        /// <param name="onlyWithBPSteps">If set to <c>true</c>, will return menus only with BP steps.</param>
        /// <returns>A menu list.</returns>
        /// <summary>
        /// returns report menu for logged user
        /// </summary>
        /// <returns>A report list.</returns>
        /// <summary>
        /// returns report menu for logged user
        /// </summary>
        /// <param name="systemContractTypeID">The system contract type ID.</param>
        /// <returns>A report list.</returns>
    }
    public class CwnetServiceInner : ICustomWareNETInner
    {
        public decimal CalculateInterestAmount(ICWObject value)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateInterestAmount(ICWObject value, decimal mainRateValue)
        {
            throw new NotImplementedException();
        }

        public T Clone<T>(T source) where T : ICWObject
        {
            throw new NotImplementedException();
        }

        public void Delete(IList value)
        {
            throw new NotImplementedException();
        }

        public void Delete(ICWObject value)
        {
            throw new NotImplementedException();
        }

        public void Export(ICWObject value)
        {
            throw new NotImplementedException();
        }

        public void Export(ICWObject value, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public ICWObject Find(object id, Type type)
        {
            throw new NotImplementedException();
        }

        public DateTime GetActualDate()
        {
            throw new NotImplementedException();
        }

        public int GetApplicationServerID()
        {
            throw new NotImplementedException();
        }

        public DateTime GetCurrentDate()
        {
            throw new NotImplementedException();
        }

        public TimeSpan GetCurrentTime()
        {
            throw new NotImplementedException();
        }

        public IList Load(Type type)
        {
            throw new NotImplementedException();
        }

        public object LoadObject(Type type, object id)
        {
            throw new NotImplementedException();
        }

        public decimal MainRateValue(ICWObject value)
        {
            throw new NotImplementedException();
        }

        public ICWObject New(Type type)
        {
            throw new NotImplementedException();
        }

        public T New<T>(bool clearFields = true)
        {
            throw new NotImplementedException();
        }

        public IList New(Type type, int count)
        {
            throw new NotImplementedException();
        }

        public ICWObject New(Type type, bool clearFields)
        {
            throw new NotImplementedException();
        }

        public ICWObject Save(ICWObject value)
        {
            throw new NotImplementedException();
        }

        public IList Save(IList value)
        {
            throw new NotImplementedException();
        }

        public IList Search(ICWObject sample)
        {
            throw new NotImplementedException();
        }

        public List<OT> SearchGeneric<OT>(OT sample) where OT : ICWObject
        {
            throw new NotImplementedException();
        }

        public T SearchOne<T>(T sample, bool retSample = false) where T : ICWObject
        {
            throw new NotImplementedException();
        }

        public T SearchOneByCode<T>(string code) where T : ICWObject
        {
            throw new NotImplementedException();
        }

        List<T> ICustomWareNETInner.Load<T>()
        {
            throw new NotImplementedException();
        }

        T ICustomWareNETInner.LoadObject<T>(object id)
        {
            throw new NotImplementedException();
        }

        T ICustomWareNETInner.LoadObject<T>(object id, bool fromDatabase)
        {
            throw new NotImplementedException();
        }

        T ICustomWareNETInner.LoadOrCreate<T>(object id)
        {
            throw new NotImplementedException();
        }

        T ICustomWareNETInner.SaveG<T>(T value)
        {
            throw new NotImplementedException();
        }
    }
}
