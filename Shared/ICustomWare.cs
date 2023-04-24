using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface ICustomWare
    {
        [OperationContract]
        void Logout();
        [OperationContract]
        void Delete(Type type, object id);
        [OperationContract]
        void SetClientData(string languageID, int terminalID, string machineName);
        [OperationContract]
        void SetServerData(string languageID, int terminalID, string machineName, bool updateLoginDate);

        [OperationContract]
        bool IsSessionLocalLang();

        //[OperationContract]
        bool IsStarted { get; }
        [OperationContract]
        byte[] GetPublicKey();

        [OperationContract]
        void ResetServiceCache();

        [OperationContract]
        string[] GetLoggedInUsers();
        [OperationContract]
        void LogMessage(byte typeID, int threadID, string message, Exception ex);
    }
}
