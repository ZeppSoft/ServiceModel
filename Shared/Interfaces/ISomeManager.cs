using MessagePack;
using MessagePack.Formatters;
using Shared.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    [ServiceContract]
    // [MessagePackFormatter(typeof(SomeManagerFormatter))]
    // [MessagePackFormatter(typeof(TypelessFormatter))]
    public interface ISomeManager
    {
        [OperationContract]
         int Sum(int x, int y);

        [OperationContract]
        string getName(int personId);

        [OperationContract]
        IList GetAmounts (string la);

        [OperationContract]
        IList GetPenalties(string la);

        [OperationContract]
        PenaltyAmount GetPenalty(string la);

        [OperationContract]
        ICWObject GetCWObject(string la);

        [OperationContract]
        Complex GetComplexObject();

        [OperationContract]
        ICWObject GetTestPerson();
    }



  

    //[MessagePackFormatter(typeof(RepaymentAmountFormatter))]
    // [MessagePackObject(keyAsPropertyName: true)]
    //[MessagePackFormatter(typeof(TypelessFormatter))]

    // [MessagePackObject]
    public class RepaymentAmount
    {
     //   [Key(0)]
        public decimal Amount { get; set; }
      //  [Key(1)]
        public decimal Tax { get; set; }

    }

    public class PenaltyAmount
    {
        public decimal Amount { get; set;}
        public decimal Tax { get; set;} 
    }


   
    public class TestPerson : ICWObject
    {
        public string FirstName { get; set;}
        public string LastName { get; set;}

        public bool IsDirty => throw new NotImplementedException();

        public bool WasRemoved => throw new NotImplementedException();

        public object ID { get ; set; }
        public string Title { get; set; }

        public void SetUpdated()
        {
            throw new NotImplementedException();
        }

        public void SetUpdated(bool value)
        {
            throw new NotImplementedException();
        }
    }

    public class TestPersonSerialized : ICWObject
    {
        public string Name { get; set; }

        public bool IsDirty => throw new NotImplementedException();

        public bool WasRemoved => throw new NotImplementedException();

        public object ID { get; set; }
        public string Title { get; set; }

        public void SetUpdated()
        {
            throw new NotImplementedException();
        }

        public void SetUpdated(bool value)
        {
            throw new NotImplementedException();
        }
    }
}
