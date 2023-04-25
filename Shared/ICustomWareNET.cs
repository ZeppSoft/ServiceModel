using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface ICustomWareNET : ICustomWare
    {
        [OperationContract]
        int GetApplicationServerID();
        // ICRMManager CRMManager { get; }
        //  IBankSearchManager BankSearchManager { get; }
        // ICashManagementManager CashManagementManager { get; }
        //  IProductsSystemContractType ProductsSystemContractTypeManager { get; }
        //  IChequeManager ChequeManager { get; }
        //   IServiceJobLauncher ScheduleLauncher { get; }
        //   IClientVerificationManager ClientVerification { get; }

        //  IDigitalSignaturesManager DigitalSignaturesManager { get; }
        //  IFIMIManager FIMIManager { get; }
        [OperationContract]
        TimeSpan GetCurrentTime();
        [OperationContract]
        DateTime GetCurrentDate();
        [OperationContract]
        DateTime GetActualDate();

        // IMapListSettings AprMappingConfiguration { get; }
        [OperationContract]
        decimal MainRateValue(ICWObject value);
        [OperationContract]
        decimal CalculateInterestAmount(ICWObject value);
        [OperationContract]
        decimal CalculateInterestAmounts(ICWObject value, decimal mainRateValue);
        /// <summary>
        /// returns service parameter
        /// see <see cref="T:Quipu.CustomWareNET.Interfaces.Objects.IServiceParameter">IServiceParameter</see>
        /// </summary>
        /// <returns></returns>
        //  IServiceParameter GetServiceParameter();
        //  void SetServiceParameter(IServiceParameter value);
        /// <summary>
        /// exports object (anExport procedure will be called)
        /// </summary>
        /// <param name="value"></param>
        /// 
        [OperationContract]
        void Export(ICWObject value);
        /// <summary>
        /// Exports the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameters">The parameters.</param>
        [OperationContract]
        void Export(ICWObject value, params object[] parameters);
        /// <summary>
        /// search for objects that are equals to the sample
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        [OperationContract]
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
        // IList Search(string searchCondition, Type objectType, params IParameterNamed[] parameters);
        //      /// <summary>
        //      /// search for objects by search condition
        //      /// </summary>
        //      /// <typeparam name="OT"></typeparam>
        //      /// <param name="searchCondition"></param>
        //      /// <returns></returns>
        //[Obsolete("sqli")]
        //      System.Collections.Generic.IList<OT> SearchGenericString<OT>(string searchCondition) where OT : ICWObject;
        // System.Collections.Generic.IList<OT> SearchGenericString<OT>(string searchCondition, params IParameterNamed[] parameters) where OT : ICWObject;
        /// <summary>
        /// searches one object from list that meets criteria
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        [OperationContract]
        T SearchOne<T>(T sample, bool retSample = false) where T : ICWObject;
        /// <summary>
        /// searches object with interface IHaveCode by it's Code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        T SearchOneByCode<T>(string code) where T : ICWObject;//, IHaveCode;
        /// <summary>
        /// loads objects from database|list
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <returns></returns>
        [OperationContract]
        IList Load(Type type);
        /// <summary>
        /// loads objects from database|list
        /// </summary>
        /// <typeparam name="T">type of objects to load</typeparam>
        /// <returns></returns>
        [OperationContract]
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
        [OperationContract]
        T LoadObject<T>(object id) where T : class, ICWObject;
        /// <summary>
        /// loads object from the database
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        object LoadObject(Type type, object id);
        /// <summary>
        /// loads object from database
        /// </summary>
        /// <typeparam name="T">type or interface</typeparam>
        /// <param name="id"></param>
        /// <param name="fromDatabase">if true - object will be reloaded</param>
        /// <returns></returns>
        [OperationContract]
        T LoadObject<T>(object id, bool fromDatabase) where T : class, ICWObject;
        /// <summary>
        /// loads or create object from the database
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
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
       // IBusinessProcessActionArgs Save(IBusinessProcessActionArgs saveArgs);

        [OperationContract]
        ICWObject Save(ICWObject value);
        [OperationContract]
        T SaveG<T>(T value) where T : class, ICWObject;
        /// <summary>
        /// saves object list to the database
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        IList Save(IList value);
        /// <summary>
        /// deletes objects in the database
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void Delete(IList value);
        /// <summary>
        /// deletes object in the database
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void Delete(ICWObject value);
        /// <summary>
        /// loads object with specified id from database/list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">type or interface</param>
        /// <returns></returns>
        [OperationContract]
        ICWObject Find(object id, Type type);
        /// <summary>
        /// new object specified type
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <returns></returns>
        [OperationContract]
        ICWObject New(Type type);
        /// <summary>
        /// new object of specified type
        /// </summary>
        /// <typeparam name="T">type or interface</typeparam>
        /// <param name="clearFields">reset property values or not</param>
        /// <returns></returns>
        [OperationContract]
        T New<T>(bool clearFields = true);
        /// <summary>
        /// clones object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [OperationContract]
        T Clone<T>(T source) where T : ICWObject;
        /// <summary>
        /// new objects list 
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <param name="count">how many new objects will be returned</param>
        /// <returns></returns>
        [OperationContract]
        IList New(Type type, int count);
        /// <summary>
        /// new object of the specified type|interface 
        /// </summary>
        /// <param name="type">type or interface</param>
        /// <param name="clearFields">object property values will be set to default values if true</param>
        /// <returns></returns>
        [OperationContract]
        ICWObject New(Type type, bool clearFields);
        /// <summary>
        /// returns application menu for logged user
        /// </summary>
        /// <returns></returns>
        //System.Collections.Generic.IList<IMenuItemInfoTree> GetApplicationMenu();
        /// <summary>
        /// Gets the application menu.
        /// </summary>
        /// <param name="onlyWithBPSteps">If set to <c>true</c>, will return menus only with BP steps.</param>
        /// <returns>A menu list.</returns>
        // System.Collections.Generic.IList<IMenuItemInfoTree> GetApplicationMenu(Boolean onlyWithBPSteps);
        /// <summary>
        /// returns report menu for logged user
        /// </summary>
        /// <returns>A report list.</returns>
        // System.Collections.Generic.IList<IMenuItemInfoTree> GetReportMenu();
        /// <summary>
        /// returns report menu for logged user
        /// </summary>
        /// <param name="systemContractTypeID">The system contract type ID.</param>
        /// <returns>A report list.</returns>
        //  System.Collections.Generic.IList<IMenuItemInfoTree> GetReportMenu(int systemContractTypeID);

        //  System.Collections.Generic.IList<IMenuItemInfoTree> GetReportMenu(IMenuItem cwObject);

        //   IBusinessProcessManager BusinessProcessManager { get; }
        //  IUserManager UserManager { get; }
        //  IAmountManager AmountManager { get; }
        //   ISecurityManager SecurityManager { get; }
        //    IContractManager ContractManager { get; }
        //    ILoanManager LoanManager { get; }
        //   ILoanOriginationManager LoanOriginationManager { get; }
        //    ICollateralManager CollateralManager { get; }
        //    IFrameworkAgreementManager FrameworkAgreementManager { get; }
        //      IFinancialLimitManager FinancialLimitManager { get; }
        //     IScheduleManager ScheduleManager { get; }
        //    IServiceJobHistory ServiceJobHistory { get; }

        //  IClientManager ClientManager { get; }
#if DEBUG
        /// <summary>
        /// will stop application service
        /// </summary>
        [OperationContract]
        void Stop();
#endif
     //   ISpecialManager SpecialManager { get; }
        [OperationContract]
        void CheckVersion(Version client, TimeZone tz);



        [OperationContract]
        IList GetParams(IList<IListParams> pars, IListParams par);


        //    IClientTransaction BeginTransaction();

        //    IMoneyTransfersManager MoneyTransferManager { get; }


        /// <summary>
        /// Electronic Payment System support manager
        /// </summary>
        //  IEPSSupportManager EPSSupportManager { get; }

        //   IEBankingSupportManager EBankingSupportManager { get; }

        //    IEBankingLoginManagementManager EBankingLoginManagementManager { get; }

        //   IEBankingAuthenticatorManagementManager EBankingAuthenticatorManagementManager { get; }


        /// <summary>
        /// Manager for CustomWare.NET Extentions
        /// </summary>
        //  IPOSTerminalManager POSTerminalManager { get; }

        //   IDirectDebitOrderManager DirectDebitOrderManager { get; }

        //    IEmbargoManager EmbargoManager { get; }

        //   IFingerPrintManager FingerPrintManager { get; }

        //    IMBankingManager MBankingManager { get; }

        //    IAuthenticatorServiceContractManager AuthenticatorServiceContract { get; }

        // IRevenueService RevenueService { get; }

        /// <summary>
        /// Url manager for Intercomputer web service
        /// </summary>
        //  IIntercomputerUrlManager UrlManager { get; }

        //    IDmsManager DmsManager { get; }
        //     ISwiftManager SwiftManager { get; }
        //   ISMEmulatorManager SMEmulatorManager { get; }
        //    IStateMachineManager StateMachine { get; }
        //    ILAStateMachineManager LAStateMachine { get; }

        //      IPaymentService PaymentService { get; }
        [OperationContract]
      (IList list, decimal repaymentAmount) GetLoanOneByOnePaymentSplitTest(string contractNumber,  decimal repaymentAmount, decimal penaltyAmount, string paymentCurrency, DateTime? date);

        [OperationContract]
        IList GetLoanOneByOnePaymentSplit(string contractNumber, ref decimal repaymentAmount, decimal penaltyAmount, string paymentCurrency, DateTime? date);
    }

    [MessagePackFormatter(typeof(ListParamFormatter))]
    public interface IListParams
    {
         int Id { get; set; }
         string Name { get; set; }    
    }

    public class ListParams : IListParams
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
