using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel
{
    public class CustomWareService : ICustomWareNET
    {
        private ICustomWareNETInner inner = new CwnetServiceInner();
        public bool IsStarted => throw new NotImplementedException();
        public void Export(ICWObject value, params object[] parameters)
        {
            string test = "Do something";
        }
        public IList GetLoanOneByOnePaymentSplit(string contractNumber, ref decimal repaymentAmount, decimal penaltyAmount, string paymentCurrency, DateTime? date)
        {
            throw new NotImplementedException();
        }
        public (IList list, decimal repaymentAmount) GetLoanOneByOnePaymentSplitTest(string contractNumber,  decimal repaymentAmount, decimal penaltyAmount, string paymentCurrency, DateTime? date)
        {
            IList list =  new List<string> { "Hello" };
            decimal result = 200;
            return (list, result);//Tuple.Create(list, result);
        }
        public IList GetParams(IList<IListParams> pars, IListParams par)
        {
            var ls = new List<int>();
            ls.Add(23);

            return ls;
        }
        public decimal CalculateInterestAmount(ICWObject value)
        {

            var obj = value as TestCWObject;
            obj.SetUpdated(true);


            // throw new NotImplementedException();
            return Convert.ToDecimal( obj.ID);
        }

        public decimal CalculateInterestAmounts(ICWObject value, decimal mainRateValue)
        {
            throw new NotImplementedException();
        }

        public void CheckVersion(Version client, TimeZone tz)
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

        public void Delete(Type type, object id)
        {
            throw new NotImplementedException();
        }

        public void Export(ICWObject value)
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

        public string[] GetLoggedInUsers()
        {
            throw new NotImplementedException();
        }

       

        public byte[] GetPublicKey()
        {
            throw new NotImplementedException();
        }

        public bool IsSessionLocalLang()
        {
            throw new NotImplementedException();
        }

        public IList Load(Type type)
        {
            throw new NotImplementedException();
        }

        public object LoadObject(Type type, object id)
        {
            /*
            //Using reflection
            var baseMethod = typeof(ICustomWareNETInner).GetMethod(nameof (ICustomWareNETInner.LoadOrCreate));
            var genericMethod = baseMethod.MakeGenericMethod(type);

            object[] arr = new object[1];
            arr[0] = id;

            var v = genericMethod.Invoke(inner, arr);
            */
            // https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod?view=netframework-4.8#System_Type_GetMethod_System_String_System_Type___
            //var baseMethod = typeof(ICustomWareNET).GetMethod(nameof(ICustomWareNET.LoadOrCreate));
            var baseMethod = typeof(ICustomWareNET).GetMethod(nameof(ICustomWareNET.LoadObject),
                 BindingFlags.Public | BindingFlags.Instance,
                  null,
                   CallingConventions.Any,
                   new Type[] { typeof(object)},
                   null);
            
            var genericMethod = baseMethod.MakeGenericMethod(type);

            object[] arr = new object[1];
            arr[0] = id;

            var v = genericMethod.Invoke(this, arr);
            return v;


           // var t = inner.LoadObject<ICWObject>(id,true);
           // return t;
        }

        public void LogMessage(byte typeID, int threadID, string message, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Logout()
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

        public void ResetServiceCache()
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

        public void SetClientData(string languageID, int terminalID, string machineName)
        {
            throw new NotImplementedException();
        }

        public void SetServerData(string languageID, int terminalID, string machineName, bool updateLoginDate)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        List<T> ICustomWareNET.Load<T>()
        {
            throw new NotImplementedException();
        }

        T ICustomWareNET.LoadObject<T>(object id)
        {
            var obj = Activator.CreateInstance<T>();
            return obj as T;
        }

        T ICustomWareNET.LoadObject<T>(object id, bool fromDatabase)
        {
            var obj = Activator.CreateInstance<T>();
            return obj as T;
        }

        T ICustomWareNET.LoadOrCreate<T>(object id)
        {
            var obj = Activator.CreateInstance<T>();
            return obj as T;
        }

        T ICustomWareNET.SaveG<T>(T value)
        {
            throw new NotImplementedException();
        }

        public IList Export(ICWObject value, string Name)
        {
            return new List<object>();
        }






        public IList GetClientContracts(long clientID, string currency, string sample)
        {
            return new List<int>();
        }

        public IList GetClientContracts(long clientID)
        {
            return new List<double>();

        }

        public IList GetClientContracts(long clientID, int[] contractStatusIDs)
        {
            return new List<float>();

        }

        public IList GetClientContracts(long clientID, int[] contractStatusIDs, int[] systemContractTypeIDs)
        {
            return new List<decimal>();

        }

        public IList GetClientContracts(long clientID, int[] contractStatusIDs, int[] systemContractTypeIDs, int[] contractTypeIDs)
        {
            return new List<string>();

        }

        public IList GetClientContracts(long clientID, int[] contractStatusIDs, int[] systemContractTypeIDs, int[] contractTypeIDs, int topContracts, int[] customerContractRelationTypesIDs = null)
        {
            return new List<object>();
        }
    }
}
