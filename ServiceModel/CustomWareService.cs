﻿using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel
{
    public class CustomWareService : ICustomWareNET
    {
        public bool IsStarted => throw new NotImplementedException();

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
            //throw new NotImplementedException();
            return new object();
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
            throw new NotImplementedException();
        }

        T ICustomWareNET.LoadObject<T>(object id, bool fromDatabase)
        {
            throw new NotImplementedException();
        }

        T ICustomWareNET.LoadOrCreate<T>(object id)
        {
            throw new NotImplementedException();
        }

        T ICustomWareNET.SaveG<T>(T value)
        {
            throw new NotImplementedException();
        }
    }
}